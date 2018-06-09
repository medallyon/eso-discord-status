using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    public class Settings : Dictionary<string, object>
    {
        static string SaveLocation = Environment.ExpandEnvironmentVariables("%TEMP%\\ESO Discord Rich Presence Settings");
        static string FileName = "EsoDiscordSettings.txt";

        static Settings DefaultSettings = new Settings
        {
            CustomEsoLocation = "",
            Enabled = true,
            ShowCharacterName = true,
            ShowPartyInfo = true,
            ToTray = true,
            StayTopMost = true
        };

        public string CustomEsoLocation
        {
            get
            {
                if (this.ContainsKey("CustomEsoLocation"))
                    return (string)this["CustomEsoLocation"];
                return Settings.DefaultSettings.CustomEsoLocation;
            }
            set
            {
                this["CustomEsoLocation"] = value;
            }
        }

        public bool Enabled
        {
            get
            {
                if (this.ContainsKey("Enabled"))
                    return Convert.ToBoolean(this["Enabled"]);
                return Settings.DefaultSettings.Enabled;
            }
            set
            {
                this["Enabled"] = value;
            }
        }

        public bool ShowCharacterName
        {
            get
            {
                if (this.ContainsKey("ShowCharacterName"))
                    return Convert.ToBoolean(this["ShowCharacterName"]);
                return Settings.DefaultSettings.ShowCharacterName;
            }
            set
            {
                this["ShowCharacterName"] = value;
            }
        }

        public bool ShowPartyInfo
        {
            get
            {
                if (this.ContainsKey("ShowPartyInfo"))
                    return Convert.ToBoolean(this["ShowPartyInfo"]);
                return Settings.DefaultSettings.ShowPartyInfo;
            }
            set
            {
                this["ShowPartyInfo"] = value;
            }
        }

        public bool ToTray
        {
            get
            {
                return Convert.ToBoolean(this["ToTray"]);
            }
            set
            {
                this["ToTray"] = value;
            }
        }

        public bool StayTopMost
        {
            get
            {
                return Convert.ToBoolean(this["StayTopMost"]);
            }
            set
            {
                this["StayTopMost"] = value;
            }
        }

        public string[] RawSettings
        {
            get
            {
                string[] raw = new string[this.Count];
                int count = 0;
                foreach (KeyValuePair<string, object> setting in this)
                    raw[count++] = $"{setting.Key}={setting.Value}";
                return raw;
            }
        }

        public Settings()
        {
            
        }

        public Settings(Dictionary<string, object> initialData)
        {
            this.Concat(initialData);
        }

        public Settings(string[] initialData)
        {
            foreach (string keyValuePair in initialData)
            {
                string[] data = keyValuePair.Split('=');
                if (data.Length == 2)
                    this.Add(data[0], data[1]);
            }
        }

        public static bool Exists()
        {
            if (File.Exists($@"{Settings.SaveLocation}\\{Settings.FileName}"))
                return true;
            return false;
        }

        public static Settings ReadFromFile()
        {
            if (!Settings.Exists())
                return new Settings();

            string[] existingSettings = null;
            try
            {
                existingSettings = File.ReadAllLines($@"{Settings.SaveLocation}\\{Settings.FileName}");
            }

            catch (IOException error)
            {
                Console.WriteLine($"{error}: {error.Message}");
            }
            if (existingSettings != null)
                return new Settings(existingSettings);
            else
                return new Settings();
        }

        public void Restore()
        {
            // populate this object with default values
            Settings.DefaultSettings.ToList().ForEach(x => this[x.Key] = x.Value);
            if (!Settings.Exists())
                return;

            try
            {
                // populate and overwrite this object with user-defined values
                Settings.ReadFromFile().ToList().ForEach(x => this[x.Key] = x.Value);
            }

            catch (IOException error)
            {
                Console.WriteLine($"{error}: {error.Message}");
            }
        }

        public void SaveToFile()
        {
            if (!Directory.Exists(Settings.SaveLocation))
                Directory.CreateDirectory(Settings.SaveLocation);

            try
            {
                File.WriteAllLines($@"{Settings.SaveLocation}\\{Settings.FileName}", this.RawSettings);
            }
            
            catch (IOException error)
            {
                Console.WriteLine($"{error}: {error.Message}");
            }
        }
    }
}
