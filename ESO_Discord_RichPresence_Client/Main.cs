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

namespace ESO_Discord_RichPresence_Client
{
    public partial class Main : Form
    {
        public static readonly string DISCORD_CLIENT_ID = "453713122713272331";
        public static readonly string ESO_STEAM_APP_ID = "306130";
        public static readonly string ADDON_NAME = "DiscordRichPresence";
        private static bool OnceMinimisedToTray = false;

        public Settings Settings;
        private SavedVariables SavedVars;
        private Discord DiscordClient;

        public bool EsoIsRunning { get; set; } = false;

        public Main()
        {
            InitializeComponent();

            this.Settings = new Settings();
            this.Settings.Restore();

            this.DiscordClient = new Discord(this, DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            this.SavedVars = new SavedVariables(this, this.DiscordClient, this.FolderBrowser);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "ESO Discord Status";

            this.InitialiseSettings();
            this.SavedVars.Initialise();

            this.InitEsoTimer();
        }

        private void InitialiseSettings()
        {
            this.Box_Enabled.Checked = this.Settings.Enabled;
            this.Box_CharacterName.Checked = this.Settings.ShowCharacterName;
            this.Box_ShowGroup.Checked = this.Settings.ShowPartyInfo;
            this.Box_ToTray.Checked = this.Settings.ToTray;
            this.Box_StayTopMost.Checked = this.Settings.StayTopMost;
        }

        private void InitEsoTimer()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(UpdateEsoText);
            timer.Interval = 1000;
            timer.Start();
        }

        private void UpdateEsoText(object sender, EventArgs e)
        {
            Process[] pName = Process.GetProcessesByName("eso64");

            if (pName.Length > 0 && !this.EsoIsRunning)
            {
                this.EsoIsRunning = true;
                this.DiscordClient.Enable();
                this.DiscordClient.UpdatePresence();

                this.Label_EsoIsRunning.ForeColor = Color.LimeGreen;
                this.Label_EsoIsRunning.Font = new Font(this.Label_EsoIsRunning.Font, FontStyle.Regular);
                this.Label_EsoIsRunning.Text = "ESO is running!\nYour status is being updated.";
            }

            else if (pName.Length == 0 && this.EsoIsRunning)
            {
                this.EsoIsRunning = false;
                this.DiscordClient.Disable();

                this.Label_EsoIsRunning.ForeColor = Color.Firebrick;
                this.Label_EsoIsRunning.Font = new Font(this.Label_EsoIsRunning.Font, FontStyle.Regular);
                this.Label_EsoIsRunning.Text = "ESO isn't running!\nYour status won't be updated.";
            }

            else if (pName.Length == 0)
            {
                this.Label_EsoIsRunning.ForeColor = Color.Firebrick;
                this.Label_EsoIsRunning.Font = new Font(this.Label_EsoIsRunning.Font, FontStyle.Regular);
                this.Label_EsoIsRunning.Text = "ESO isn't running!\nYour status won't be updated.";
            }
        }

        private void Box_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Box_Enabled.Checked && !this.Settings.Enabled && Discord.CurrentCharacter != null)
            {
                this.DiscordClient.Enable();
                this.DiscordClient.UpdatePresence();
            }

            else if (!this.Box_Enabled.Checked && this.Settings.Enabled && Discord.CurrentCharacter != null)
                this.DiscordClient.Disable();

            this.Settings.Enabled = this.Box_Enabled.Checked;
            this.Settings.SaveToFile();
        }

        private void Box_CharacterName_CheckedChanged(object sender, EventArgs e)
        {
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();

            this.Settings.ShowCharacterName = this.Box_CharacterName.Checked;
            this.Settings.SaveToFile();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();

            this.Settings.ShowPartyInfo = this.Box_ShowGroup.Checked;
            this.Settings.SaveToFile();
        }

        private void Box_ToTray_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.ToTray = this.Box_ToTray.Checked;
            this.Settings.SaveToFile();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && this.Settings.ToTray)
                this.MinimiseToTray();
        }

        private void MinimiseToTray()
        {
            this.NotifyIcon1.Visible = true;
            this.Hide();

            if (!Main.OnceMinimisedToTray)
            {
                Main.OnceMinimisedToTray = true;
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

            this.Settings.StayTopMost = this.TopMost;
            this.Settings.SaveToFile();
        }
    }
}
