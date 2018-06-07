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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Box_Enabled = new System.Windows.Forms.CheckBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.Box_CharacterName = new System.Windows.Forms.CheckBox();
            this.Box_ShowGroup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Box_Enabled
            // 
            this.Box_Enabled.AutoSize = true;
            this.Box_Enabled.Checked = true;
            this.Box_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_Enabled.Location = new System.Drawing.Point(25, 27);
            this.Box_Enabled.Name = "Box_Enabled";
            this.Box_Enabled.Size = new System.Drawing.Size(65, 17);
            this.Box_Enabled.TabIndex = 0;
            this.Box_Enabled.Text = "Enabled";
            this.Box_Enabled.UseVisualStyleBackColor = true;
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
            this.Box_CharacterName.Location = new System.Drawing.Point(25, 51);
            this.Box_CharacterName.Name = "Box_CharacterName";
            this.Box_CharacterName.Size = new System.Drawing.Size(125, 17);
            this.Box_CharacterName.TabIndex = 1;
            this.Box_CharacterName.Text = "Use Character Name";
            this.Box_CharacterName.UseVisualStyleBackColor = true;
            this.Box_CharacterName.CheckedChanged += new System.EventHandler(this.Box_CharacterName_CheckedChanged);
            // 
            // Box_ShowGroup
            // 
            this.Box_ShowGroup.AutoSize = true;
            this.Box_ShowGroup.Checked = true;
            this.Box_ShowGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Box_ShowGroup.Location = new System.Drawing.Point(25, 75);
            this.Box_ShowGroup.Name = "Box_ShowGroup";
            this.Box_ShowGroup.Size = new System.Drawing.Size(106, 17);
            this.Box_ShowGroup.TabIndex = 2;
            this.Box_ShowGroup.Text = "Show Group Info";
            this.Box_ShowGroup.UseVisualStyleBackColor = true;
            this.Box_ShowGroup.CheckedChanged += new System.EventHandler(this.Box_ShowGroup_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 161);
            this.Controls.Add(this.Box_ShowGroup);
            this.Controls.Add(this.Box_CharacterName);
            this.Controls.Add(this.Box_Enabled);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "ESO Discord Rich Presence Client";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox Box_Enabled;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.CheckBox Box_CharacterName;
        private System.Windows.Forms.CheckBox Box_ShowGroup;
    }
}

