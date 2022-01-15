using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ESO_Discord_RichPresence_Client
{
    public partial class Main : Form
    {
        public const string DISCORD_CLIENT_ID = "453713122713272331";
        public const string ESO_STEAM_APP_ID = "306130";
        public const string ADDON_NAME = "DiscordStatus";

        public Settings Settings;
        private Timer _esoTimer;
        private Timer _closeTimer;
        private SavedVariables _savedVars;
        private Discord _discordClient;
        private SteamAppIdForm _steamAppIdForm;

        public int StartTimestamp { get; set; } = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        public bool EsoIsRunning { get; set; }
        public bool EsoRanOnce { get; set; }
        public bool JustMinimized { get; set; }

        public Process EsoProcess
        {
            get
            {
                var pName = Process.GetProcessesByName("eso64");
                return pName.Length > 0 ? pName[0] : null;
            }
        }
        public Process EsoLauncherProcess
        {
            get
            {
                var pName = Process.GetProcessesByName("Bethesda.net_Launcher");
                return pName.Length > 0 ? pName[0] : null;
            }
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Settings = new Settings();

            HandleDuplicateClient();

            _discordClient = new Discord(this, DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            _savedVars = new SavedVariables(this, _discordClient, FolderBrowser);

            CreateSteamAppIdForm();
            InitialiseSettings();
            _savedVars.Initialise();

            InitEsoTimers();
        }

        private void InitialiseSettings()
        {
            Box_Enabled.Checked = (bool)Settings.Get("Enabled");
            Box_CharacterName.Checked = (bool)Settings.Get("ShowCharacterName");
            Box_ShowGroup.Checked = (bool)Settings.Get("ShowPartyInfo");
            Box_ToTray.Checked = (bool)Settings.Get("ToTray");
            Box_StayTopMost.Checked = (bool)Settings.Get("StayTopMost");
            Box_AutoStart.Checked = (bool)Settings.Get("AutoStart");
            Box_AutoExit.Checked = (bool)Settings.Get("AutoExit");
            Box_CloseLauncher.Checked = (bool)Settings.Get("CloseLauncher");

            if (Settings.Get("CustomSteamAppID") != null)
                _steamAppIdForm.Controls.Find("SteamIdTextBox", true)[0].Text = (string)Settings.Get("CustomSteamAppID");

            if ((bool)Settings.Get("AutoStart"))
                StartESO();
        }

        private void InitEsoTimers()
        {
            _esoTimer = new Timer { Interval = 1000 };
            _esoTimer.Tick += UpdateClientStatus;
            _esoTimer.Start();

            _closeTimer = new Timer { Interval = 1000 };
            _closeTimer.Tick += CloseTimer_Tick;
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            _closeTimer.Stop();
            EsoLauncherProcess?.Kill();
        }

        private void HandleDuplicateClient()
        {
            Process[] defaultProcesses = Process.GetProcessesByName("ESO_Discord_RichPresence_Client")
                , customProcesses = Process.GetProcessesByName("DiscordStatusClient");

            // Concatenate arrays
            var runningProcesses = new Process[defaultProcesses.Length + customProcesses.Length];
            defaultProcesses.CopyTo(runningProcesses, 0);
            customProcesses.CopyTo(runningProcesses, defaultProcesses.Length);

            if (runningProcesses.Length == 1 || !(bool)Settings.Get("AutoStart"))
                return;

            StartESO();
            Application.Exit();
        }

        private void CreateSteamAppIdForm()
        {
            _steamAppIdForm = new SteamAppIdForm();
            _steamAppIdForm.Hide();

            UpdateSteamAppIdFormLocation();
            
            Control textBoxControl = _steamAppIdForm.Controls.Find("SteamIdTextBox", true).First();
            textBoxControl.KeyDown += AppIdTextBox_KeyDown;
            textBoxControl.LostFocus += AppIdTextBox_LostFocus;
        }

        private void UpdateSteamAppIdFormLocation()
        {
            _steamAppIdForm.Location = new Point(
                Location.X + (Width / 2) - (_steamAppIdForm.Width / 2),
                Location.Y + (Height / 3) - (_steamAppIdForm.Height / 2)
            );
        }

        private void StartESO()
        {
            if (EsoProcess != null)
                return;

            if (Settings.Get("CustomSteamAppID") != null && Settings.Get("CustomSteamAppID").ToString().Length >= 5)
            {
                Process.Start($"steam://rungameid/{(string)Settings.Get("CustomSteamAppID")}");
                return;
            }

            const string steamBaseKey = "SOFTWARE\\WOW6432Node\\Valve\\Steam";
            const string esoBaseKey = "SOFTWARE\\WOW6432Node\\Zenimax_Online\\Launcher";

            RegistryKey steamRegistryKey = Registry.LocalMachine.OpenSubKey(steamBaseKey);
            RegistryKey esoRegistryKey;

            // Steam is installed
            if (steamRegistryKey != null)
            {
                string steamEsoBaseKey = $"{steamBaseKey}\\Apps\\{ESO_STEAM_APP_ID}";
                esoRegistryKey = Registry.LocalMachine.OpenSubKey(steamEsoBaseKey);

                // ESO is installed through Steam
                if (esoRegistryKey != null)
                {
                    Process.Start($"steam://rungameid/{ESO_STEAM_APP_ID}");
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

                if (FolderBrowser.ShowDialog() == DialogResult.OK)
                {
                    esoInstallLocation = FolderBrowser.SelectedPath;
                    Settings.Set("CustomEsoInstallLocation", FolderBrowser.SelectedPath);
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

        public void UpdateStatusField(string status, Color newColor, FontStyle style = FontStyle.Regular)
        {
            if (Label_EsoIsRunning.InvokeRequired)
            {
                Label_EsoIsRunning.Invoke(new MethodInvoker(delegate {
                    Label_EsoIsRunning.ForeColor = newColor;
                    Label_EsoIsRunning.Font = new Font(Label_EsoIsRunning.Font, style);
                    Label_EsoIsRunning.Text = status;
                }));
            }

            else
            {
                Label_EsoIsRunning.ForeColor = newColor;
                Label_EsoIsRunning.Font = new Font(Label_EsoIsRunning.Font, style);
                Label_EsoIsRunning.Text = status;
            }
        }

        private void UpdateClientStatus(object sender, EventArgs e)
        {
            if (!SavedVariables.Exists)
            {
                UpdateStatusField("Type '/reloadui' into the ESO chat box, then wait.", Color.Goldenrod, FontStyle.Bold);
                return;
            }

            bool processExists = EsoProcess != null;
            if (processExists && !EsoIsRunning)
            {
                EsoIsRunning = true;
                _discordClient.Enable();

                StartTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                _discordClient.UpdatePresence();
            }
            else if (!processExists && EsoIsRunning || !processExists)
            {
                EsoIsRunning = false;
                _discordClient.Disable();
            }

            if (EsoIsRunning)
            {
                EsoRanOnce = true;

                if ((bool)Settings.Get("CloseLauncher"))
                {
                    // Close the launcher after 1 second
                    _closeTimer.Start();
                }

                if (!JustMinimized)
                {
                    JustMinimized = true;
                    Minimize();
                }

                UpdateStatusField("ESO is running!\nYour status is being updated.", Color.LimeGreen);
            }
            else
            {
                if ((bool)Settings.Get("AutoExit") && EsoRanOnce)
                    Application.Exit();

                JustMinimized = false;

                UpdateStatusField("ESO isn't running!\nYour status won't be updated.", Color.Firebrick);
            }

            TrayContextMenu.Items["startEsoToolStripMenuItem"].Enabled = !EsoIsRunning;
        }

        public void InstallAddon()
        {
            string addonDirectory = $@"{SavedVariables.EsoDir}\live\AddOns\{ADDON_NAME}";

            if (Directory.Exists(addonDirectory))
                Directory.Delete(addonDirectory, true);
            Directory.CreateDirectory(addonDirectory);

            UnpackAddonFiles.Unpack(addonDirectory);
        }

        #region Settings Changes

        private void Box_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Enabled.Checked && !(bool)Settings.Get("Enabled") && Discord.CurrentCharacter != null)
            {
                _discordClient.Enable();
                _discordClient.UpdatePresence();
            }

            else if (!Box_Enabled.Checked && (bool)Settings.Get("Enabled") && Discord.CurrentCharacter != null)
                _discordClient.Disable();

            Settings.Set("Enabled", Box_Enabled.Checked);
        }

        private void Box_CharacterName_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("ShowCharacterName", Box_CharacterName.Checked);
            if (Discord.CurrentCharacter != null)
                _discordClient.UpdatePresence();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("ShowPartyInfo", Box_ShowGroup.Checked);
            if (Discord.CurrentCharacter != null)
                _discordClient.UpdatePresence();
        }

        private void Box_ToTray_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("ToTray", Box_ToTray.Checked);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && (bool)Settings.Get("ToTray"))
                MinimizeToTray();
        }

        private void Minimize()
        {
            if ((bool)Settings.Get("ToTray"))
                MinimizeToTray();
            else
                WindowState = FormWindowState.Minimized;
        }

        private void MinimizeToTray()
        {
            TrayIcon.Visible = true;
            Hide();

            if ((bool)Settings.Get("MinimizedOnce"))
                return;

            Settings.Set("MinimizedOnce", true);
            TrayIcon.ShowBalloonTip(2000);
        }

        private void Box_StayTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = Box_StayTopMost.Checked;

            Settings.Set("StayTopMost", TopMost);
        }

        private void Box_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("AutoStart", Box_AutoStart.Checked);
        }

        private void Box_CloseLauncher_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("CloseLauncher", Box_CloseLauncher.Checked);
        }

        private void Box_AutoExit_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("AutoExit", Box_AutoExit.Checked);
        }

        private void Box_AutoStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TopMost = false;
            UpdateSteamAppIdFormLocation();
            _steamAppIdForm.Show();
        }

        private void AppIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;

            Control steamIdTextBox = _steamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            Settings.Set("CustomSteamAppID", steamIdTextBox.Text);

            TopMost = (bool)Settings.Get("StayTopMost");
            _steamAppIdForm.Hide();
        }

        private void AppIdTextBox_LostFocus(object sender, EventArgs e)
        {
            Control steamIdTextBox = _steamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            Settings.Set("CustomSteamAppID", steamIdTextBox.Text);

            TopMost = (bool)Settings.Get("StayTopMost");
            _steamAppIdForm.Hide();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            
        }

        #endregion Settings Changes

        private void ShowClient()
        {
            Show();
            WindowState = FormWindowState.Normal;
            TrayIcon.Visible = false;
        }

        #region Tray Icon

        private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e) => ShowClient();

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TrayContextMenu.Show(Cursor.Position);
        }

        #region Tray Context Menu

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrayContextMenu.Hide();
            ShowClient();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void StartEsoToolStripMenuItem_Click(object sender, EventArgs e) => StartESO();

        #endregion Tray Context Menu

        #endregion Tray Icon
    }
}
