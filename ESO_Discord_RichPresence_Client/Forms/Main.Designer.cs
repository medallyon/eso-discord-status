namespace ESO_Discord_RichPresence_Client
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Box_Enabled = new System.Windows.Forms.CheckBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.Box_CharacterName = new System.Windows.Forms.CheckBox();
            this.Box_ShowGroup = new System.Windows.Forms.CheckBox();
            this.Box_StayTopMost = new System.Windows.Forms.CheckBox();
            this.Label_EsoIsRunning = new System.Windows.Forms.Label();
            this.Box_ToTray = new System.Windows.Forms.CheckBox();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startEsoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Box_AutoStart = new System.Windows.Forms.CheckBox();
            this.Box_AutoExit = new System.Windows.Forms.CheckBox();
            this.Tooltip_Main = new System.Windows.Forms.ToolTip(this.components);
            this.Box_CloseLauncher = new System.Windows.Forms.CheckBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Box_Enabled
            // 
            this.Box_Enabled.AccessibleDescription = "Whether Discord Rich Presence should be shown on your profile.";
            this.Box_Enabled.AccessibleName = "Enabled";
            this.Box_Enabled.AutoSize = true;
            this.Box_Enabled.Checked = true;
            this.Box_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_Enabled.Location = new System.Drawing.Point(25, 75);
            this.Box_Enabled.Name = "Box_Enabled";
            this.Box_Enabled.Size = new System.Drawing.Size(65, 17);
            this.Box_Enabled.TabIndex = 0;
            this.Box_Enabled.Text = "Enabled";
            this.Tooltip_Main.SetToolTip(this.Box_Enabled, "Whether Discord Rich Presence should be shown on your profile.");
            this.Box_Enabled.UseVisualStyleBackColor = true;
            this.Box_Enabled.CheckedChanged += new System.EventHandler(this.Box_Enabled_CheckedChanged);
            // 
            // FolderBrowser
            // 
            this.FolderBrowser.ShowNewFolderButton = false;
            // 
            // Box_CharacterName
            // 
            this.Box_CharacterName.AccessibleDescription = "Displays your active character\'s name if enabled or your account name if disabled" +
    ".";
            this.Box_CharacterName.AccessibleName = "Use Character Name";
            this.Box_CharacterName.AutoSize = true;
            this.Box_CharacterName.Checked = true;
            this.Box_CharacterName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_CharacterName.Location = new System.Drawing.Point(25, 98);
            this.Box_CharacterName.Name = "Box_CharacterName";
            this.Box_CharacterName.Size = new System.Drawing.Size(125, 17);
            this.Box_CharacterName.TabIndex = 1;
            this.Box_CharacterName.Text = "Use Character Name";
            this.Tooltip_Main.SetToolTip(this.Box_CharacterName, "Displays your active character\'s name if enabled or your account name if disabled" +
        ".");
            this.Box_CharacterName.UseVisualStyleBackColor = true;
            this.Box_CharacterName.CheckedChanged += new System.EventHandler(this.Box_CharacterName_CheckedChanged);
            // 
            // Box_ShowGroup
            // 
            this.Box_ShowGroup.AccessibleDescription = "Displays your active Party if enabled. Has no effect if you\'re not in a Party.";
            this.Box_ShowGroup.AccessibleName = "Show Party Info";
            this.Box_ShowGroup.AutoSize = true;
            this.Box_ShowGroup.Checked = true;
            this.Box_ShowGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_ShowGroup.Location = new System.Drawing.Point(25, 121);
            this.Box_ShowGroup.Name = "Box_ShowGroup";
            this.Box_ShowGroup.Size = new System.Drawing.Size(101, 17);
            this.Box_ShowGroup.TabIndex = 2;
            this.Box_ShowGroup.Text = "Show Party Info";
            this.Tooltip_Main.SetToolTip(this.Box_ShowGroup, "Displays your active Party if enabled. Has no effect if you\'re not in a Party.");
            this.Box_ShowGroup.UseVisualStyleBackColor = true;
            this.Box_ShowGroup.CheckedChanged += new System.EventHandler(this.Box_ShowGroup_CheckedChanged);
            // 
            // Box_StayTopMost
            // 
            this.Box_StayTopMost.AccessibleDescription = "Makes this client stay on top of other windows.";
            this.Box_StayTopMost.AccessibleName = "Stay On Top";
            this.Box_StayTopMost.AutoSize = true;
            this.Box_StayTopMost.Checked = true;
            this.Box_StayTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_StayTopMost.Location = new System.Drawing.Point(206, 75);
            this.Box_StayTopMost.Name = "Box_StayTopMost";
            this.Box_StayTopMost.Size = new System.Drawing.Size(86, 17);
            this.Box_StayTopMost.TabIndex = 3;
            this.Box_StayTopMost.Text = "Stay On Top";
            this.Tooltip_Main.SetToolTip(this.Box_StayTopMost, "Makes this client stay on top of other windows.");
            this.Box_StayTopMost.UseVisualStyleBackColor = true;
            this.Box_StayTopMost.CheckedChanged += new System.EventHandler(this.Box_StayTopMost_CheckedChanged);
            // 
            // Label_EsoIsRunning
            // 
            this.Label_EsoIsRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_EsoIsRunning.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_EsoIsRunning.Location = new System.Drawing.Point(25, 18);
            this.Label_EsoIsRunning.Name = "Label_EsoIsRunning";
            this.Label_EsoIsRunning.Size = new System.Drawing.Size(301, 33);
            this.Label_EsoIsRunning.TabIndex = 4;
            this.Label_EsoIsRunning.Text = "Evaluating if ESO is running...";
            this.Label_EsoIsRunning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Tooltip_Main.SetToolTip(this.Label_EsoIsRunning, "The current status of your Presence.");
            // 
            // Box_ToTray
            // 
            this.Box_ToTray.AccessibleDescription = "Minimizes this client to the tray.";
            this.Box_ToTray.AccessibleName = "Minimize to tray";
            this.Box_ToTray.AutoSize = true;
            this.Box_ToTray.Checked = true;
            this.Box_ToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_ToTray.Location = new System.Drawing.Point(25, 144);
            this.Box_ToTray.Name = "Box_ToTray";
            this.Box_ToTray.Size = new System.Drawing.Size(98, 17);
            this.Box_ToTray.TabIndex = 5;
            this.Box_ToTray.Text = "Minimize to tray";
            this.Tooltip_Main.SetToolTip(this.Box_ToTray, "Minimizes this client to the tray.");
            this.Box_ToTray.UseVisualStyleBackColor = true;
            this.Box_ToTray.CheckedChanged += new System.EventHandler(this.Box_ToTray_CheckedChanged);
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipText = "This program has now been minimized to the tray. Double-click to bring me back up" +
    "!";
            this.TrayIcon.BalloonTipTitle = "Discord Status Updater";
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Discord Status Updater";
            this.TrayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseClick);
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayIcon_MouseDoubleClick);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.startEsoToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(138, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // startEsoToolStripMenuItem
            // 
            this.startEsoToolStripMenuItem.Name = "startEsoToolStripMenuItem";
            this.startEsoToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.startEsoToolStripMenuItem.Text = "Launch ESO";
            this.startEsoToolStripMenuItem.Click += new System.EventHandler(this.StartEsoToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // Box_AutoStart
            // 
            this.Box_AutoStart.AccessibleDescription = "Whether ESO should automatically be started with this client.";
            this.Box_AutoStart.AccessibleName = "Auto Start ESO";
            this.Box_AutoStart.AutoSize = true;
            this.Box_AutoStart.Checked = true;
            this.Box_AutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_AutoStart.Location = new System.Drawing.Point(206, 98);
            this.Box_AutoStart.Name = "Box_AutoStart";
            this.Box_AutoStart.Size = new System.Drawing.Size(98, 17);
            this.Box_AutoStart.TabIndex = 6;
            this.Box_AutoStart.Text = "Auto Start ESO";
            this.Tooltip_Main.SetToolTip(this.Box_AutoStart, "Whether ESO should automatically be started with this client.\r\nRight click this t" +
        "o enter a custom Steam ID for a Steam shortcut you\'ve created.");
            this.Box_AutoStart.UseVisualStyleBackColor = true;
            this.Box_AutoStart.CheckedChanged += new System.EventHandler(this.Box_AutoStart_CheckedChanged);
            this.Box_AutoStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Box_AutoStart_MouseClick);
            // 
            // Box_AutoExit
            // 
            this.Box_AutoExit.AccessibleDescription = "Automatically closes this client when you exit ESO.";
            this.Box_AutoExit.AccessibleName = "Auto Exit";
            this.Box_AutoExit.AutoSize = true;
            this.Box_AutoExit.Checked = true;
            this.Box_AutoExit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_AutoExit.Location = new System.Drawing.Point(206, 144);
            this.Box_AutoExit.Name = "Box_AutoExit";
            this.Box_AutoExit.Size = new System.Drawing.Size(68, 17);
            this.Box_AutoExit.TabIndex = 7;
            this.Box_AutoExit.Text = "Auto Exit";
            this.Tooltip_Main.SetToolTip(this.Box_AutoExit, "Automatically closes this client when you exit ESO.");
            this.Box_AutoExit.UseVisualStyleBackColor = true;
            this.Box_AutoExit.CheckedChanged += new System.EventHandler(this.Box_AutoExit_CheckedChanged);
            // 
            // Box_CloseLauncher
            // 
            this.Box_CloseLauncher.AccessibleDescription = "Automatically exits the launcher after the game has been launched.";
            this.Box_CloseLauncher.AccessibleName = "Auto Close Launcher";
            this.Box_CloseLauncher.AutoSize = true;
            this.Box_CloseLauncher.Checked = true;
            this.Box_CloseLauncher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_CloseLauncher.Location = new System.Drawing.Point(206, 121);
            this.Box_CloseLauncher.Name = "Box_CloseLauncher";
            this.Box_CloseLauncher.Size = new System.Drawing.Size(125, 17);
            this.Box_CloseLauncher.TabIndex = 8;
            this.Box_CloseLauncher.Text = "Auto Close Launcher";
            this.Tooltip_Main.SetToolTip(this.Box_CloseLauncher, "Automatically exits the launcher after the game has been launched.");
            this.Box_CloseLauncher.UseVisualStyleBackColor = true;
            this.Box_CloseLauncher.CheckedChanged += new System.EventHandler(this.Box_CloseLauncher_CheckedChanged);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(25, 191);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(101, 23);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset Documents";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(354, 246);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.Box_CloseLauncher);
            this.Controls.Add(this.Box_AutoExit);
            this.Controls.Add(this.Box_AutoStart);
            this.Controls.Add(this.Box_ToTray);
            this.Controls.Add(this.Label_EsoIsRunning);
            this.Controls.Add(this.Box_StayTopMost);
            this.Controls.Add(this.Box_ShowGroup);
            this.Controls.Add(this.Box_CharacterName);
            this.Controls.Add(this.Box_Enabled);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Discord Rich Presence for ESO";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Box_Enabled;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.CheckBox Box_CharacterName;
        private System.Windows.Forms.CheckBox Box_ShowGroup;
        private System.Windows.Forms.CheckBox Box_StayTopMost;
        private System.Windows.Forms.Label Label_EsoIsRunning;
        private System.Windows.Forms.CheckBox Box_ToTray;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.CheckBox Box_AutoStart;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startEsoToolStripMenuItem;
        private System.Windows.Forms.CheckBox Box_AutoExit;
        private System.Windows.Forms.ToolTip Tooltip_Main;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.CheckBox Box_CloseLauncher;
        private System.Windows.Forms.Button ResetButton;
    }
}

