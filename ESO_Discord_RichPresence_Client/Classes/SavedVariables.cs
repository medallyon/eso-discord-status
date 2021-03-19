using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLua;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace ESO_Discord_RichPresence_Client
{
    class SavedVariables
    {
        static public bool Exists = false;
        static public string esoDir = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Elder Scrolls Online");
        static public string Dir
        {
            get
            {
                return $@"{SavedVariables.esoDir}\live\SavedVariables";
            }
        }

        internal readonly Main Main;
        private readonly Discord _client;
        private readonly FolderBrowserDialog _browser;
        private readonly FileSystemWatcher _watcher;

        public string Path
        {
            get
            {
                return $@"{SavedVariables.Dir}\{Main.ADDON_NAME}.lua";
            }
        }

        public SavedVariables(Main form, Discord client, FolderBrowserDialog browser)
        {
            this.Main = form;
            this._client = client;
            this._browser = browser;
            this._watcher = new FileSystemWatcher();
        }

        public void Initialise()
        {
            SavedVariables.esoDir = (string)this.Main.Settings.Get("CustomEsoLocation");

            this.EnsureSavedVarsExist();
            this.SetupWatcher();

            if (!SavedVariables.Exists)
                return;

            string LuaContents = File.ReadAllText(this.Path);
            Discord.CurrentCharacter = SavedVariables.ParseLua(LuaContents);
        }

        static public EsoCharacter ParseLua(string luaTable)
        {
            var LuaClient = new Lua();
            object[] result = null;

            try
            {
                result = LuaClient.DoString(luaTable);
            }

            catch (NLua.Exceptions.LuaException error)
            {
                var luaErrorResponse = MessageBox.Show($"Something went wrong while reading data from ESO: {error.Message}", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                if (luaErrorResponse == DialogResult.Retry)
                    ParseLua(luaTable);
                else if (luaErrorResponse == DialogResult.Abort)
                    Application.Exit();
            }

            LuaTable rootTable = LuaClient.GetTable($"{Main.ADDON_NAME}_SavedVars");
            LuaTable defaultTable = (LuaTable)rootTable["Default"];

            Dictionary<object, LuaTable> Accounts = new Dictionary<object, LuaTable>();
            foreach (object key in defaultTable.Keys)
            {
                LuaTable value = (LuaTable)defaultTable[key];
                Accounts.Add(key, value);
            }

            return new EsoCharacter((LuaTable)Accounts.Values.First()["$AccountWide"]);
        }

        public void EnsureSavedVarsExist()
        {
            // "Elder Scrolls Online" doesn't exist in "My Documents"
            if (!Directory.Exists(SavedVariables.esoDir))
            {
                var response = MessageBox.Show("ESO Documents not found. Please select the \"Elder Scrolls Online\" folder in your Documents.", "File not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                if (response == DialogResult.OK)
                {
                    if (this._browser.ShowDialog() == DialogResult.OK)
                    {
                        SavedVariables.esoDir = this._browser.SelectedPath;
                        this.Main.Settings.Set("CustomEsoLocation", SavedVariables.esoDir);
                    }

                    else
                        Environment.Exit(1);
                }

                else
                    Environment.Exit(1);
            }

            // if lua file doesn't exist in "SavedVariables"
            if (!File.Exists($@"{SavedVariables.Dir}\{Main.ADDON_NAME}.lua"))
            {
                // if eso addon doesn't exist
                if (!Directory.Exists($@"{SavedVariables.esoDir}\live\AddOns\{Main.ADDON_NAME}"))
                {
                    var addonResponse = MessageBox.Show($"The \"{Main.ADDON_NAME}\" AddOn was not detected in your Addons Folder. Do you want to install the addon and try again?", "AddOn Missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    if (addonResponse == DialogResult.OK)
                    {
                        this.Main.InstallAddon();
                        this.EnsureSavedVarsExist();
                    }
                    else
                        Environment.Exit(1);
                }

                else
                {
                    // Ensure that the AddOn is up-to-date.
                    this.Main.InstallAddon();

                    SavedVariables.Exists = false;
                    this.Main.UpdateStatusField("Type '/reloadui' into the ESO chat box, then wait.", Color.Goldenrod, FontStyle.Bold);
                }
            }

            else
                SavedVariables.Exists = true;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void SetupWatcher()
        {
            this._watcher.Path = $@"{SavedVariables.Dir}";
            this._watcher.Filter = $"{Main.ADDON_NAME}.lua";
            this._watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

            this._watcher.Created += new FileSystemEventHandler(this.OnChanged);
            this._watcher.Changed += new FileSystemEventHandler(this.OnChanged);
            this._watcher.Deleted += new FileSystemEventHandler(this.OnDeleted);
            this._watcher.Renamed += new RenamedEventHandler(this.OnRenamed);

            this._watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("SavedVariables file changed or created");
            SavedVariables.Exists = true;
            
            // wait 1 second here to avoid conflicts with file being busy
            Thread.Sleep(1000);

            try
            {
                string LuaCharacter = File.ReadAllText(e.FullPath);
                Discord.CurrentCharacter = SavedVariables.ParseLua(LuaCharacter);
                this._client.Enable();
                this._client.UpdatePresence(Discord.CurrentCharacter);
            }

            catch (System.IO.IOException error)
            {
                var errorResponse = MessageBox.Show($"Something happened while updating your game: {error.Message}", "File Read Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                if (errorResponse == DialogResult.Abort)
                    Environment.Exit(1);
                else if (errorResponse == DialogResult.Retry)
                    this.OnChanged(source, e);
            }
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("SavedVariables file deleted");
            SavedVariables.Exists = false;
            this._client.Disable();

            this.EnsureSavedVarsExist();
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine("SavedVariables file renamed");
            SavedVariables.Exists = false;
            this._client.Disable();

            this.EnsureSavedVarsExist();
        }
    }
}
