namespace ESO_Discord_RichPresence_Client
{
    partial class SteamAppIdForm
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
            this.SteamIdTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SteamIdTextBox
            // 
            this.SteamIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SteamIdTextBox.Location = new System.Drawing.Point(3, 10);
            this.SteamIdTextBox.Name = "SteamIdTextBox";
            this.SteamIdTextBox.Size = new System.Drawing.Size(130, 20);
            this.SteamIdTextBox.TabIndex = 0;
            // 
            // SteamAppIdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(28)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(136, 40);
            this.ControlBox = false;
            this.Controls.Add(this.SteamIdTextBox);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SteamAppIdForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SteamIdTextBox;
    }
}