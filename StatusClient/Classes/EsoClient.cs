using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DiscordStatus
{
    public class EsoClient : AbstractProcess
    {
        public bool IsRunning { get; private set; }

        private readonly string _steamAppID;
        private readonly FolderBrowserDialog _folderBrowser;

        public event EventHandler Started;
        public event EventHandler Exited;

        private Timer _tick;

        public EsoClient(string steamAppID, FolderBrowserDialog folderBrowser)
        {
            ProcessName = "eso64";
            _steamAppID = (steamAppID ?? string.Empty).Replace("steam://rungameid/", "");
            _folderBrowser = folderBrowser;

            _tick = new Timer
            {
                Interval = 1000,
                Enabled = true
            };

            _tick.Tick += (s, e) =>
            {
                if (Exists && !IsRunning)
                {
                    IsRunning = true;
                    Started?.Invoke(this, e);
                }

                else if (!Exists && IsRunning)
                {
                    IsRunning = false;
                    Exited?.Invoke(this, e);
                }
            };
        }

        /// <summary>
        /// Attempts to launch the Bethesda.net_Launcher.exe.
        /// </summary>
        public void Start()
        {
            if (Exists)
                return;

            if (!string.IsNullOrEmpty(_steamAppID) && _steamAppID.Length >= 5)
            {
                Process.Start($"steam://rungameid/{_steamAppID}");
                return;
            }

            const string steamBaseKey = "SOFTWARE\\WOW6432Node\\Valve\\Steam";
            const string esoBaseKey = "SOFTWARE\\WOW6432Node\\Zenimax_Online\\Launcher";

            RegistryKey steamRegistryKey = Registry.LocalMachine.OpenSubKey(steamBaseKey);
            RegistryKey esoRegistryKey;

            // Steam is installed
            if (steamRegistryKey != null)
            {
                string steamEsoBaseKey = $"{steamBaseKey}\\Apps\\{Main.ESO_STEAM_APP_ID}";
                esoRegistryKey = Registry.LocalMachine.OpenSubKey(steamEsoBaseKey);

                // ESO is installed through Steam
                if (esoRegistryKey != null)
                {
                    Process.Start($"steam://rungameid/{Main.ESO_STEAM_APP_ID}");
                    return;
                }
            }

            esoRegistryKey = Registry.LocalMachine.OpenSubKey(esoBaseKey);
            string esoInstallLocation;

            // ESO cannot be found
            if (esoRegistryKey == null)
            {
                DialogResult response = MessageBox.Show("ESO could not be found. Please select your ESO folder.", "ESO not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (response == DialogResult.Cancel)
                    return;

                if (_folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    esoInstallLocation = _folderBrowser.SelectedPath;
                    Main.Settings.Set("CustomEsoInstallLocation", _folderBrowser.SelectedPath);
                }
                else
                    return;
            }

            // ESO is installed
            else
                esoInstallLocation = (string)esoRegistryKey.GetValue("InstallPath");

            string esoLauncherPath = $"{esoInstallLocation}\\Launcher\\Bethesda.net_Launcher.exe";
            if (!File.Exists(esoLauncherPath))
            {
                MessageBox.Show("ESO could not be found. Please make sure ESO is installed correctly, then try again.", "ESO not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = esoLauncherPath,
                WorkingDirectory = $"{esoInstallLocation}\\Launcher"
            });
        }
    }
}