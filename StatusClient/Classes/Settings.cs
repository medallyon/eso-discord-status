using System;
using System.Collections.Generic;
using System.Linq;

namespace ESO_Discord_RichPresence_Client
{
    public class Settings : Dictionary<string, object>
    {
        private static readonly Dictionary<string, object> Default = new Dictionary<string, object>
        {
            { "CustomEsoLocation", string.Empty },
            { "CustomEsoInstallLocation", string.Empty },
            { "CustomSteamAppID", string.Empty },
            { "Enabled", true },
            { "ShowCharacterName", true },
            { "ShowPartyInfo", true },
            { "ToTray", true },
            { "StayTopMost", true },
            { "AutoStart", false },
            { "AutoExit", false },
            { "CloseLauncher", false },
            { "MinimizedOnce", false }
        };

        private readonly Properties.Settings _actualSettings = Properties.Settings.Default;

        public new object this[string key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                _actualSettings[key] = value;
                _actualSettings.Save();
            }
        }

        public Settings()
        {
            Initialize();
        }
        public Settings(Dictionary<string, object> initialData)
        {
            Initialize();
            _ = this.Concat(initialData);
        }
        public Settings(IEnumerable<string> initialData)
        {
            Initialize();
            foreach (string keyValuePair in initialData)
            {
                string[] data = keyValuePair.Split('=');
                if (data.Length == 2)
                    Set(data[0], data[1]);
            }
        }

        private void Initialize()
        {
            // populate the current Settings object
            foreach (var setting in Default)
            {
                if (!Has(setting.Key) && _actualSettings[setting.Key] != null)
                    Set(setting.Key, _actualSettings[setting.Key]);
            }
        }

        public bool Has(string setting)
        {
            return ContainsKey(setting);
        }

        public object Get(string setting)
        {
            return Has(setting) ? this[setting] : null;
        }

        public void Set(string setting, object value)
        {
            this[setting] = value;
        }
    }
}
