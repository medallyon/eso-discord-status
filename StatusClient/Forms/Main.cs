using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DiscordStatus
{
    public partial class Main : Form
    {
        public const string DISCORD_CLIENT_ID = "453713122713272331";
        public const string ESO_STEAM_APP_ID = "306130";
        public const string ADDON_NAME = "DiscordStatus";

        public static Settings Settings;
        public static Timer CloseLauncherTimer;
        public static SavedVariables SavedVars;
        public static Discord DiscordClient;
        public static SteamAppIdForm SteamAppIdForm;
        public static EsoClient EsoClient;
        public static AbstractProcess EsoLauncher = new AbstractProcess("Bethesda.net_Launcher");

        public static int StartTimestamp { get; set; } = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        public static bool EsoRanOnce { get; set; }
        public static bool JustMinimized { get; set; }

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The entry point for this application.
        /// </summary>
        private void Main_Load(object sender, EventArgs e)
        {
            Settings = new Settings();

            EsoClient = new EsoClient((string)Settings.Get("CustomSteamAppID") ?? ESO_STEAM_APP_ID, FolderBrowser);
            EsoClient.Started += (s, x) => OnEsoStarted();
            EsoClient.Exited += (s, x) => OnEsoExited();

            DiscordClient = new Discord(this, DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            SavedVars = new SavedVariables(this);

            HandleDuplicateClient();
            CreateSteamAppIdForm();
            InitialiseClientFromSettings();
            SavedVars.Initialise();

            { // Initialise the CloseLauncher Timer, but don't start it yet
                CloseLauncherTimer = new Timer { Interval = 1500 };
                CloseLauncherTimer.Tick += CloseLauncherTimer_Tick;
            }

            if (EsoClient.Exists)
                OnEsoStarted();
            else
                OnEsoExited();
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

            EsoClient.Start();
            Application.Exit();
        }

        private void CreateSteamAppIdForm()
        {
            SteamAppIdForm = new SteamAppIdForm();
            SteamAppIdForm.Hide();

            UpdateSteamAppIdFormLocation();

            Control textBoxControl = SteamAppIdForm.Controls.Find("SteamIdTextBox", true).First();
            textBoxControl.KeyDown += AppIdTextBox_KeyDown;
            textBoxControl.LostFocus += AppIdTextBox_LostFocus;
        }

        private void UpdateSteamAppIdFormLocation()
        {
            SteamAppIdForm.Location = new Point(
                Location.X + (Width / 2) - (SteamAppIdForm.Width / 2),
                Location.Y + (Height / 3) - (SteamAppIdForm.Height / 2)
            );
        }

        /// <summary>
        /// Populates the Client's checkboxes and text fields from the Settings object.
        /// </summary>
        private void InitialiseClientFromSettings()
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
                SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0].Text = (string)Settings.Get("CustomSteamAppID");

            if ((bool)Settings.Get("AutoStart"))
                EsoClient.Start();
        }

        private static void CloseLauncherTimer_Tick(object sender, EventArgs e)
        {
            CloseLauncherTimer.Stop();
            EsoLauncher.Process?.Kill();
        }

        private void OnEsoStarted()
        {
            if (!SavedVariables.Exists)
            {
                UpdateStatusField("Type '/reloadui' into the ESO chat box, then wait.", Color.Goldenrod, FontStyle.Bold);
                return;
            }

            EsoRanOnce = true;
            StartTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            DiscordClient.Enable();
            DiscordClient.UpdatePresence();
            UpdateStatusField("ESO is running!\nYour status is being updated.", Color.LimeGreen);

            if ((bool)Settings.Get("CloseLauncher"))
            {
                // Close the launcher after 1.5 seconds
                CloseLauncherTimer.Start();
            }

            // Minimise the Client if it's not already minimized
            if (!JustMinimized)
            {
                JustMinimized = true;
                Minimize();
            }

            PlayButton.Enabled = false;
            TrayContextMenu.Items["startEsoToolStripMenuItem"].Enabled = false;
        }

        private void OnEsoExited()
        {
            if (!SavedVariables.Exists)
            {
                UpdateStatusField("Type '/reloadui' into the ESO chat box, then wait.", Color.Goldenrod, FontStyle.Bold);
                return;
            }

            DiscordClient.Disable();
            UpdateStatusField("ESO isn't running!\nYour status won't be updated.", Color.Firebrick);

            if ((bool)Settings.Get("AutoExit") && EsoRanOnce)
            {
                Application.Exit();
                return;
            }

            JustMinimized = false;

            PlayButton.Enabled = true;
            TrayContextMenu.Items["startEsoToolStripMenuItem"].Enabled = true;
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

        public void InstallAddon()
        {
            string addonDirectory = $@"{SavedVariables.EsoPath}\live\AddOns\{ADDON_NAME}";
            DirectoryInfo addonDir = new DirectoryInfo(addonDirectory);

            addonDir.Create();
            foreach (FileInfo file in addonDir.GetFiles())
                file.Delete();

            UnpackAddonFiles.Unpack(addonDirectory);
        }

        #region Settings Changes

        private void Box_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (Box_Enabled.Checked && !(bool)Settings.Get("Enabled") && Discord.CurrentCharacter != null)
            {
                DiscordClient.Enable();
                DiscordClient.UpdatePresence();
            }

            else if (!Box_Enabled.Checked && (bool)Settings.Get("Enabled") && Discord.CurrentCharacter != null)
                DiscordClient.Disable();

            Settings.Set("Enabled", Box_Enabled.Checked);
        }

        private void Box_CharacterName_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("ShowCharacterName", Box_CharacterName.Checked);
            if (Discord.CurrentCharacter != null)
                DiscordClient.UpdatePresence();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Set("ShowPartyInfo", Box_ShowGroup.Checked);
            if (Discord.CurrentCharacter != null)
                DiscordClient.UpdatePresence();
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
            SteamAppIdForm.Show();
        }

        private void AppIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;

            Control steamIdTextBox = SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            Settings.Set("CustomSteamAppID", steamIdTextBox.Text);

            TopMost = (bool)Settings.Get("StayTopMost");
            SteamAppIdForm.Hide();
        }

        private void AppIdTextBox_LostFocus(object sender, EventArgs e)
        {
            Control steamIdTextBox = SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            Settings.Set("CustomSteamAppID", steamIdTextBox.Text);

            TopMost = (bool)Settings.Get("StayTopMost");
            SteamAppIdForm.Hide();
        }

        private void PlayButton_Click(object sender, EventArgs e) => EsoClient.Start();

        private void ResetButton_Click(object sender, EventArgs e) => SavedVars.Reset();

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

        private void StartEsoToolStripMenuItem_Click(object sender, EventArgs e) => EsoClient.Start();

        #endregion Tray Context Menu

        #endregion Tray Icon
    }
}
