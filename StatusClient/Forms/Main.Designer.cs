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
            System.Windows.Forms.Label PresenceLabel;
            System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
            System.Windows.Forms.Label SettingsLabel;
            System.Windows.Forms.Label BehaviourLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.Label ActionsLabel;
            System.Windows.Forms.Panel panel1;
            this.Box_AutoStart = new System.Windows.Forms.CheckBox();
            this.Box_AutoExit = new System.Windows.Forms.CheckBox();
            this.Box_CloseLauncher = new System.Windows.Forms.CheckBox();
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
            this.Tooltip_Main = new System.Windows.Forms.ToolTip(this.components);
            this.ResetButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.PlayButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            PresenceLabel = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            SettingsLabel = new System.Windows.Forms.Label();
            BehaviourLabel = new System.Windows.Forms.Label();
            ActionsLabel = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            flowLayoutPanel2.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // PresenceLabel
            // 
            PresenceLabel.AutoSize = true;
            PresenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            PresenceLabel.Location = new System.Drawing.Point(3, 0);
            PresenceLabel.Name = "PresenceLabel";
            PresenceLabel.Size = new System.Drawing.Size(74, 16);
            PresenceLabel.TabIndex = 10;
            PresenceLabel.Text = "Presence";
            PresenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(this.Box_AutoStart);
            flowLayoutPanel2.Controls.Add(this.Box_AutoExit);
            flowLayoutPanel2.Controls.Add(this.Box_CloseLauncher);
            flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanel2.Location = new System.Drawing.Point(0, 19);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(140, 77);
            flowLayoutPanel2.TabIndex = 12;
            // 
            // Box_AutoStart
            // 
            this.Box_AutoStart.AccessibleDescription = "Whether ESO should automatically be started with this client.";
            this.Box_AutoStart.AccessibleName = "Auto Start ESO";
            this.Box_AutoStart.AutoSize = true;
            this.Box_AutoStart.Checked = true;
            this.Box_AutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_AutoStart.Location = new System.Drawing.Point(3, 3);
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
            this.Box_AutoExit.Location = new System.Drawing.Point(3, 26);
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
            this.Box_CloseLauncher.Location = new System.Drawing.Point(3, 49);
            this.Box_CloseLauncher.Name = "Box_CloseLauncher";
            this.Box_CloseLauncher.Size = new System.Drawing.Size(125, 17);
            this.Box_CloseLauncher.TabIndex = 8;
            this.Box_CloseLauncher.Text = "Auto Close Launcher";
            this.Tooltip_Main.SetToolTip(this.Box_CloseLauncher, "Automatically exits the launcher after the game has been launched.");
            this.Box_CloseLauncher.UseVisualStyleBackColor = true;
            this.Box_CloseLauncher.CheckedChanged += new System.EventHandler(this.Box_CloseLauncher_CheckedChanged);
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            SettingsLabel.Location = new System.Drawing.Point(3, 0);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new System.Drawing.Size(64, 16);
            SettingsLabel.TabIndex = 13;
            SettingsLabel.Text = "Settings";
            SettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BehaviourLabel
            // 
            BehaviourLabel.AutoSize = true;
            BehaviourLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            BehaviourLabel.Location = new System.Drawing.Point(3, 1);
            BehaviourLabel.Name = "BehaviourLabel";
            BehaviourLabel.Size = new System.Drawing.Size(78, 16);
            BehaviourLabel.TabIndex = 15;
            BehaviourLabel.Text = "Behaviour";
            BehaviourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Box_Enabled
            // 
            this.Box_Enabled.AccessibleDescription = "Whether Discord Rich Presence should be shown on your profile.";
            this.Box_Enabled.AccessibleName = "Enabled";
            this.Box_Enabled.AutoSize = true;
            this.Box_Enabled.Checked = true;
            this.Box_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_Enabled.Location = new System.Drawing.Point(3, 3);
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
            this.Box_CharacterName.Location = new System.Drawing.Point(3, 26);
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
            this.Box_ShowGroup.Location = new System.Drawing.Point(3, 49);
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
            this.Box_StayTopMost.Location = new System.Drawing.Point(3, 3);
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
            this.Box_ToTray.Location = new System.Drawing.Point(3, 26);
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
            // ResetButton
            // 
            this.ResetButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ResetButton.Location = new System.Drawing.Point(16, 58);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(101, 23);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset Documents";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Box_Enabled);
            this.flowLayoutPanel1.Controls.Add(this.Box_CharacterName);
            this.flowLayoutPanel1.Controls.Add(this.Box_ShowGroup);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(140, 77);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.Box_StayTopMost);
            this.flowLayoutPanel3.Controls.Add(this.Box_ToTray);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 20);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(140, 49);
            this.flowLayoutPanel3.TabIndex = 14;
            // 
            // ActionsLabel
            // 
            ActionsLabel.AutoSize = true;
            ActionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ActionsLabel.Location = new System.Drawing.Point(3, 0);
            ActionsLabel.Name = "ActionsLabel";
            ActionsLabel.Size = new System.Drawing.Size(59, 16);
            ActionsLabel.TabIndex = 16;
            ActionsLabel.Text = "Actions";
            ActionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayButton
            // 
            this.PlayButton.AccessibleDescription = "Launch The Elder Scrolls Online";
            this.PlayButton.AccessibleName = "Play";
            this.PlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PlayButton.Location = new System.Drawing.Point(37, 23);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(56, 29);
            this.PlayButton.TabIndex = 10;
            this.PlayButton.Text = "PLAY";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // panel1
            // 
            panel1.Controls.Add(PresenceLabel);
            panel1.Controls.Add(this.flowLayoutPanel1);
            panel1.Location = new System.Drawing.Point(28, 56);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(140, 96);
            panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(SettingsLabel);
            this.panel2.Controls.Add(flowLayoutPanel2);
            this.panel2.Location = new System.Drawing.Point(186, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 96);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(BehaviourLabel);
            this.panel3.Controls.Add(this.flowLayoutPanel3);
            this.panel3.Location = new System.Drawing.Point(28, 158);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(140, 81);
            this.panel3.TabIndex = 19;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(ActionsLabel);
            this.panel4.Controls.Add(this.PlayButton);
            this.panel4.Controls.Add(this.ResetButton);
            this.panel4.Location = new System.Drawing.Point(186, 158);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(132, 100);
            this.panel4.TabIndex = 20;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(354, 263);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.Label_EsoIsRunning);
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
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}

