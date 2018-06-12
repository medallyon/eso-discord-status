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
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Box_AutoStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Box_Enabled
            // 
            this.Box_Enabled.AutoSize = true;
            this.Box_Enabled.Checked = true;
            this.Box_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_Enabled.Location = new System.Drawing.Point(25, 75);
            this.Box_Enabled.Name = "Box_Enabled";
            this.Box_Enabled.Size = new System.Drawing.Size(65, 17);
            this.Box_Enabled.TabIndex = 0;
            this.Box_Enabled.Text = "Enabled";
            this.Box_Enabled.UseVisualStyleBackColor = true;
            this.Box_Enabled.UseWaitCursor = true;
            this.Box_Enabled.CheckedChanged += new System.EventHandler(this.Box_Enabled_CheckedChanged);
            // 
            // FolderBrowser
            // 
            this.FolderBrowser.ShowNewFolderButton = false;
            // 
            // Box_CharacterName
            // 
            this.Box_CharacterName.AutoSize = true;
            this.Box_CharacterName.Checked = true;
            this.Box_CharacterName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_CharacterName.Location = new System.Drawing.Point(25, 98);
            this.Box_CharacterName.Name = "Box_CharacterName";
            this.Box_CharacterName.Size = new System.Drawing.Size(125, 17);
            this.Box_CharacterName.TabIndex = 1;
            this.Box_CharacterName.Text = "Use Character Name";
            this.Box_CharacterName.UseVisualStyleBackColor = true;
            this.Box_CharacterName.UseWaitCursor = true;
            this.Box_CharacterName.CheckedChanged += new System.EventHandler(this.Box_CharacterName_CheckedChanged);
            // 
            // Box_ShowGroup
            // 
            this.Box_ShowGroup.AutoSize = true;
            this.Box_ShowGroup.Checked = true;
            this.Box_ShowGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_ShowGroup.Location = new System.Drawing.Point(25, 121);
            this.Box_ShowGroup.Name = "Box_ShowGroup";
            this.Box_ShowGroup.Size = new System.Drawing.Size(101, 17);
            this.Box_ShowGroup.TabIndex = 2;
            this.Box_ShowGroup.Text = "Show Party Info";
            this.Box_ShowGroup.UseVisualStyleBackColor = true;
            this.Box_ShowGroup.UseWaitCursor = true;
            this.Box_ShowGroup.CheckedChanged += new System.EventHandler(this.Box_ShowGroup_CheckedChanged);
            // 
            // Box_StayTopMost
            // 
            this.Box_StayTopMost.AutoSize = true;
            this.Box_StayTopMost.Checked = true;
            this.Box_StayTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_StayTopMost.Location = new System.Drawing.Point(240, 121);
            this.Box_StayTopMost.Name = "Box_StayTopMost";
            this.Box_StayTopMost.Size = new System.Drawing.Size(86, 17);
            this.Box_StayTopMost.TabIndex = 3;
            this.Box_StayTopMost.Text = "Stay On Top";
            this.Box_StayTopMost.UseVisualStyleBackColor = true;
            this.Box_StayTopMost.UseWaitCursor = true;
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
            this.Label_EsoIsRunning.UseWaitCursor = true;
            // 
            // Box_ToTray
            // 
            this.Box_ToTray.AutoSize = true;
            this.Box_ToTray.Checked = true;
            this.Box_ToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_ToTray.Location = new System.Drawing.Point(240, 98);
            this.Box_ToTray.Name = "Box_ToTray";
            this.Box_ToTray.Size = new System.Drawing.Size(98, 17);
            this.Box_ToTray.TabIndex = 5;
            this.Box_ToTray.Text = "Minimise to tray";
            this.Box_ToTray.UseVisualStyleBackColor = true;
            this.Box_ToTray.UseWaitCursor = true;
            this.Box_ToTray.CheckedChanged += new System.EventHandler(this.Box_ToTray_CheckedChanged);
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.BalloonTipText = "This program has now been minimized to the tray. Double-click to bring me back up" +
    "!";
            this.NotifyIcon1.BalloonTipTitle = "Discord Status Updater";
            this.NotifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon1.Icon")));
            this.NotifyIcon1.Text = "Discord Status Updater";
            this.NotifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // Box_AutoStart
            // 
            this.Box_AutoStart.AutoSize = true;
            this.Box_AutoStart.Location = new System.Drawing.Point(240, 75);
            this.Box_AutoStart.Name = "Box_AutoStart";
            this.Box_AutoStart.Size = new System.Drawing.Size(98, 17);
            this.Box_AutoStart.TabIndex = 6;
            this.Box_AutoStart.Text = "Auto Start ESO";
            this.Box_AutoStart.UseVisualStyleBackColor = true;
            this.Box_AutoStart.UseWaitCursor = true;
            this.Box_AutoStart.CheckedChanged += new System.EventHandler(this.Box_AutoStart_CheckedChanged);
            this.Box_AutoStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Box_AutoStart_MouseClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(354, 161);
            this.Controls.Add(this.Box_AutoStart);
            this.Controls.Add(this.Box_ToTray);
            this.Controls.Add(this.Label_EsoIsRunning);
            this.Controls.Add(this.Box_StayTopMost);
            this.Controls.Add(this.Box_ShowGroup);
            this.Controls.Add(this.Box_CharacterName);
            this.Controls.Add(this.Box_Enabled);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "ESO Discord Rich Presence Client";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
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
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.CheckBox Box_AutoStart;
    }
}

