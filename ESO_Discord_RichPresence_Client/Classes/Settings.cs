using System;
using System.Collections.Generic;
using System.Linq;

namespace ESO_Discord_RichPresence_Client
{
    public class Settings : Dictionary<string, object>
    {
        static readonly Dictionary<string, object> Default = new Dictionary<string, object>
        {
            { "CustomEsoLocation", String.Empty },
            { "CustomEsoInstallLocation", String.Empty },
            { "CustomSteamAppID", String.Empty },
            { "Enabled", true },
            { "ShowCharacterName", true },
            { "ShowPartyInfo", true },
            { "ToTray", true },
            { "StayTopMost", true },
            { "AutoStart", false },
            { "AutoExit", false },
            { "MinimizedOnce", false }
        };

        private readonly Properties.Settings ActualSettings = Properties.Settings.Default;

        public new object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                this.ActualSettings[key] = value;
                this.ActualSettings.Save();
            }
        }

        public Settings()
        {
            this.Initialize();
        }

        public Settings(Dictionary<string, object> initialData)
        {
            this.Initialize();
            this.Concat(initialData);
        }

        public Settings(string[] initialData)
        {
            this.Initialize();
            foreach (string keyValuePair in initialData)
            {
                string[] data = keyValuePair.Split('=');
                if (data.Length == 2)
                    this.Set(data[0], data[1]);
            }
        }

        private Settings Initialize()
        {
            // populate the current Settings object
            foreach (KeyValuePair<string, object> setting in Settings.Default)
            {
                if (!this.Has(setting.Key) && this.ActualSettings[setting.Key] != null)
                    this.Set(setting.Key, this.ActualSettings[setting.Key]);
            }

            return this;
        }

        public bool Has(string setting)
        {
            return this.ContainsKey(setting);
        }

        public object Get(string setting)
        {
            if (this.Has(setting))
                return this[setting];
            else
                return null;
        }

        public void Set(string setting, object value)
        {
            this[setting] = value;
        }
    }
}
