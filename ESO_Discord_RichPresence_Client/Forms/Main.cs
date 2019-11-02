using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using NLua;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ESO_Discord_RichPresence_Client
{
    public partial class Main : Form
    {
        public const string DISCORD_CLIENT_ID = "453713122713272331";
        public const string ESO_STEAM_APP_ID = "306130";
        public const string ADDON_NAME = "DiscordRichPresence";

        public Settings Settings;
        private Timer EsoTimer;
        private SavedVariables SavedVars;
        private Discord DiscordClient;
        private SteamAppIdForm SteamAppIdForm;

        public bool EsoIsRunning { get; set; } = false;
        public int StartTimestamp { get; set; } = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "ESO Discord Status";

            this.Settings = new Settings();

            this.DiscordClient = new Discord(this, DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            this.SavedVars = new SavedVariables(this, this.DiscordClient, this.FolderBrowser);

            this.CreateSteamAppIdForm();
            this.InitialiseSettings();
            this.SavedVars.Initialise();

            this.InitEsoTimer();
        }

        private void CreateSteamAppIdForm()
        {
            this.SteamAppIdForm = new SteamAppIdForm();
            this.SteamAppIdForm.Hide();

            this.UpdateSteamAppIdFormLocation();
            
            var textBoxControl = this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true).First();
            textBoxControl.KeyDown += AppIdTextBox_KeyDown;
            textBoxControl.LostFocus += AppIdTextBox_LostFocus;
        }

        private void UpdateSteamAppIdFormLocation()
        {
            this.SteamAppIdForm.Location = new Point(
                this.Location.X + (this.Width / 2) - (this.SteamAppIdForm.Width / 2),
                this.Location.Y + (this.Height / 3) - (this.SteamAppIdForm.Height / 2)
            );
        }

        private void InitialiseSettings()
        {
            this.Box_Enabled.Checked = (bool)this.Settings.Get("Enabled");
            this.Box_CharacterName.Checked = (bool)this.Settings.Get("ShowCharacterName");
            this.Box_ShowGroup.Checked = (bool)this.Settings.Get("ShowPartyInfo");
            this.Box_ToTray.Checked = (bool)this.Settings.Get("ToTray");
            this.Box_StayTopMost.Checked = (bool)this.Settings.Get("StayTopMost");
            this.Box_AutoStart.Checked = (bool)this.Settings.Get("AutoStart");

            if (this.Settings.Get("CustomSteamAppID") != null)
                this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0].Text = (string)this.Settings.Get("CustomSteamAppID");

            if ((bool)this.Settings.Get("AutoStart"))
                this.StartESO();
        }

        private void StartESO()
        {
            Process[] pName = Process.GetProcessesByName("eso64");
            if (pName.Length > 0)
                return;

            if (this.Settings.Get("CustomSteamAppID") != null && this.Settings.Get("CustomSteamAppID").ToString().Length >= 5)
            {
                Process.Start($"steam://rungameid/{(string)this.Settings.Get("CustomSteamAppID")}");
                return;
            }

            const string SteamBaseKey = "SOFTWARE\\WOW6432Node\\Valve\\Steam";
            const string EsoBaseKey = "SOFTWARE\\WOW6432Node\\Zenimax_Online\\Launcher";

            RegistryKey SteamRegistryKey = Registry.LocalMachine.OpenSubKey(SteamBaseKey);
            RegistryKey EsoRegistryKey;

            // Steam is installed
            if (SteamRegistryKey != null)
            {
                string SteamEsoBaseKey = $"{SteamBaseKey}\\Apps\\{Main.ESO_STEAM_APP_ID}";
                EsoRegistryKey = Registry.LocalMachine.OpenSubKey(SteamEsoBaseKey);

                // ESO is installed through Steam
                if (EsoRegistryKey != null)
                {
                    Process.Start($"steam://rungameid/{Main.ESO_STEAM_APP_ID}");
                    return;
                }
            }

            EsoRegistryKey = Registry.LocalMachine.OpenSubKey(EsoBaseKey);
            string EsoInstallLocation = String.Empty;

            // ESO cannot be found
            if (EsoRegistryKey == null)
            {
                DialogResult response = MessageBox.Show("ESO could not be found. Please select your ESO folder.", "ESO not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (response == DialogResult.Cancel)
                    return;
                else
                {
                    if (this.FolderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        EsoInstallLocation = this.FolderBrowser.SelectedPath;
                        this.Settings.Set("CustomEsoInstallLocation", this.FolderBrowser.SelectedPath);
                    }

                    else
                        return;
                }
            }

            // ESO is installed
            else
                EsoInstallLocation = (string)EsoRegistryKey.GetValue("InstallPath");

            string EsoLauncherPath = $"{EsoInstallLocation}\\Launcher\\Bethesda.net_Launcher.exe";
            if (!File.Exists(EsoLauncherPath))
            {
                MessageBox.Show("ESO could not be found. Please make sure ESO is installed correctly, then try again.", "ESO not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            
            Process.Start(new ProcessStartInfo
            {
                FileName = EsoLauncherPath,
                WorkingDirectory = $"{EsoInstallLocation}\\Launcher"
            });
        }

        private void InitEsoTimer()
        {
            this.EsoTimer = new Timer();
            this.EsoTimer.Tick += new EventHandler(UpdateEsoText);
            this.EsoTimer.Interval = 1000;
            this.EsoTimer.Start();
        }

        private void UpdateEsoText(object sender, EventArgs e)
        {
            if (!SavedVariables.Exists)
            {
                this.UpdateStatusField("Type '/reloadui' into the ESO chat box, then wait.", Color.Goldenrod, FontStyle.Bold);
                return;
            }

            Process[] processes = Process.GetProcessesByName("eso64");

            if (processes.Length > 0 && !this.EsoIsRunning)
            {
                this.EsoIsRunning = true;
                this.DiscordClient.Enable();

                this.StartTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                this.DiscordClient.UpdatePresence();
            }

            else if ((processes.Length == 0 && this.EsoIsRunning) || processes.Length == 0)
            {
                this.EsoIsRunning = false;
                this.DiscordClient.Disable();
            }

            if (this.EsoIsRunning)
                this.UpdateStatusField("ESO is running!\nYour status is being updated.", Color.LimeGreen, FontStyle.Regular);
            else
                this.UpdateStatusField("ESO isn't running!\nYour status won't be updated.", Color.Firebrick, FontStyle.Regular);
        }

        public void UpdateStatusField(string status)
        {
            this.UpdateStatusField(status, Color.DarkGray, FontStyle.Regular);
        }

        public void UpdateStatusField(string status, Color newColor, FontStyle style)
        {
            if (this.Label_EsoIsRunning.InvokeRequired)
            {
                this.Label_EsoIsRunning.Invoke(new MethodInvoker(delegate {
                    this.Label_EsoIsRunning.ForeColor = newColor;
                    this.Label_EsoIsRunning.Font = new Font(this.Label_EsoIsRunning.Font, style);
                    this.Label_EsoIsRunning.Text = status;
                }));
            }

            else
            {
                this.Label_EsoIsRunning.ForeColor = newColor;
                this.Label_EsoIsRunning.Font = new Font(this.Label_EsoIsRunning.Font, style);
                this.Label_EsoIsRunning.Text = status;
            }
        }

        public void InstallAddon()
        {
            string AddonDirectory = $@"{SavedVariables.esoDir}\live\AddOns\{Main.ADDON_NAME}";

            if (Directory.Exists(AddonDirectory))
                Directory.Delete(AddonDirectory, true);
            Directory.CreateDirectory(AddonDirectory);

            UnpackAddonFiles.UnpackAddon(AddonDirectory);
        }

        private void Box_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Box_Enabled.Checked && !(bool)this.Settings.Get("Enabled") && Discord.CurrentCharacter != null)
            {
                this.DiscordClient.Enable();
                this.DiscordClient.UpdatePresence();
            }

            else if (!this.Box_Enabled.Checked && (bool)this.Settings.Get("Enabled") && Discord.CurrentCharacter != null)
                this.DiscordClient.Disable();

            this.Settings.Set("Enabled", this.Box_Enabled.Checked);
        }

        private void Box_CharacterName_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.Set("ShowCharacterName", this.Box_CharacterName.Checked);
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.Set("ShowPartyInfo", this.Box_ShowGroup.Checked);
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();
        }

        private void Box_ToTray_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.Set("ToTray", this.Box_ToTray.Checked);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && (bool)this.Settings.Get("ToTray"))
                this.MinimiseToTray();
        }

        private void MinimiseToTray()
        {
            this.NotifyIcon1.Visible = true;
            this.Hide();

            if (!(bool)this.Settings.Get("MinimizedOnce"))
            {
                this.Settings.Set("MinimizedOnce", true);
                this.NotifyIcon1.ShowBalloonTip(2000);
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.NotifyIcon1.Visible = false;
        }

        private void Box_StayTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.Box_StayTopMost.Checked;

            this.Settings.Set("StayTopMost", this.TopMost);
        }

        private void Box_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.Set("AutoStart", this.Box_AutoStart.Checked);
        }

        private void Box_AutoStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.TopMost = false;
                this.UpdateSteamAppIdFormLocation();
                this.SteamAppIdForm.Show();
            }
        }

        private void AppIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control SteamIdTextBox = this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
                this.Settings.Set("CustomSteamAppID", SteamIdTextBox.Text);

                this.TopMost = (bool)this.Settings.Get("StayTopMost");
                this.SteamAppIdForm.Hide();
            }
        }

        private void AppIdTextBox_LostFocus(object sender, EventArgs e)
        {
            Control SteamIdTextBox = this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            this.Settings.Set("CustomSteamAppID", SteamIdTextBox.Text);

            this.TopMost = (bool)this.Settings.Get("StayTopMost");
            this.SteamAppIdForm.Hide();
        }
    }
}
