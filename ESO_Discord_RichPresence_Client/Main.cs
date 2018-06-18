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
        public static readonly string DISCORD_CLIENT_ID = "453713122713272331";
        public static readonly string ESO_STEAM_APP_ID = "306130";
        public static readonly string ADDON_NAME = "DiscordRichPresence";
        private static bool OnceMinimisedToTray = false;

        public Settings Settings;
        private SavedVariables SavedVars;
        private Discord DiscordClient;
        private Form SteamAppIdForm;

        public bool EsoIsRunning { get; set; } = false;

        public Main()
        {
            InitializeComponent();

            this.Settings = new Settings();
            this.Settings.Restore();

            this.DiscordClient = new Discord(this, DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            this.SavedVars = new SavedVariables(this, this.DiscordClient, this.FolderBrowser);
            this.CreateSteamAppIdForm();
        }

        private void CreateSteamAppIdForm()
        {
            this.SteamAppIdForm = new Form();
            var form = this.SteamAppIdForm;
            form.Hide();

            form.Name = "SteamAppIdForm";
            form.AutoSize = false;
            form.TopLevel = true;
            form.ControlBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.BackColor = Color.FromArgb(27, 28, 31);
            form.ForeColor = this.ForeColor;
            form.WindowState = FormWindowState.Normal;
            form.MinimumSize = new Size(0, 0);
            form.Size = new Size(124, 44);
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(
                this.Location.X + this.Width - (form.Width / 2),
                this.Location.Y + (this.Height / 2) - (form.Height / 2)
            );

            TextBox AppIdTextBox = new TextBox
            {
                Name = "SteamIdTextBox",
                Enabled = true,
                Size = new Size(120, 40),
                Location = new Point(2, 2),
                BorderStyle = BorderStyle.FixedSingle
            };
            AppIdTextBox.KeyDown += AppIdTextBox_KeyDown;
            AppIdTextBox.LostFocus += AppIdTextBox_LostFocus;

            form.Controls.Add(AppIdTextBox);
        }

        private void InitialiseSettings()
        {
            this.Box_Enabled.Checked = this.Settings.Enabled;
            this.Box_CharacterName.Checked = this.Settings.ShowCharacterName;
            this.Box_ShowGroup.Checked = this.Settings.ShowPartyInfo;
            this.Box_ToTray.Checked = this.Settings.ToTray;
            this.Box_StayTopMost.Checked = this.Settings.StayTopMost;
            this.Box_AutoStart.Checked = this.Settings.AutoStart;

            if (this.Settings.CustomSteamAppID != null)
                this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0].Text = this.Settings.CustomSteamAppID;

            if (this.Settings.AutoStart)
                this.StartESO();
        }

        private void StartESO()
        {
            if (this.Settings.CustomSteamAppID != null && this.Settings.CustomSteamAppID.Length >= 5)
            {
                Process.Start($"steam://rungameid/{this.Settings.CustomSteamAppID}");
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
                        this.Settings.CustomEsoInstallLocation = this.FolderBrowser.SelectedPath;
                        this.Settings.SaveToFile();
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
            this.Settings.ShowCharacterName = this.Box_CharacterName.Checked;
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();

            this.Settings.SaveToFile();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.ShowPartyInfo = this.Box_ShowGroup.Checked;
            if (Discord.CurrentCharacter != null)
                this.DiscordClient.UpdatePresence();

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

        private void Box_AutoStart_CheckedChanged(object sender, EventArgs e)
        {
            this.Settings.AutoStart = this.Box_AutoStart.Checked;
            this.Settings.SaveToFile();
        }

        private void Box_AutoStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.TopMost = false;
                this.SteamAppIdForm.Show();
            }
        }

        private void AppIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Control SteamIdTextBox = this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
                this.Settings.CustomSteamAppID = SteamIdTextBox.Text;
                this.Settings.SaveToFile();

                this.TopMost = this.Settings.StayTopMost;
                this.SteamAppIdForm.Hide();
            }
        }

        private void AppIdTextBox_LostFocus(object sender, EventArgs e)
        {
            Control SteamIdTextBox = this.SteamAppIdForm.Controls.Find("SteamIdTextBox", true)[0];
            this.Settings.CustomSteamAppID = SteamIdTextBox.Text;
            this.Settings.SaveToFile();

            this.TopMost = this.Settings.StayTopMost;
            this.SteamAppIdForm.Hide();
        }
    }
}
