using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private SavedVariables SavedVars;
        private Discord DiscordClient;

        public Main()
        {
            InitializeComponent();

            this.DiscordClient = new Discord(DISCORD_CLIENT_ID, ESO_STEAM_APP_ID);
            this.SavedVars = new SavedVariables(this.DiscordClient, this.FolderBrowser);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "ESO Discord Rich Presence Client";

            this.DiscordClient.Enabled = true;
            this.DiscordClient.ShowCharacterName = true;
            this.DiscordClient.ShowGroupInfo = true;

            this.DiscordClient.Enable();
            this.DiscordClient.UpdatePresence(Discord.CurrentCharacter);
        }

        private void Box_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Box_Enabled.Checked && !this.DiscordClient.Enabled)
            {
                this.DiscordClient.Enable();
                this.DiscordClient.UpdatePresence();
            }

            else if (!this.Box_Enabled.Checked && this.DiscordClient.Enabled)
                this.DiscordClient.Disable();

            this.DiscordClient.Enabled = this.Box_Enabled.Checked;
        }

        private void Box_CharacterName_CheckedChanged(object sender, EventArgs e)
        {
            this.DiscordClient.ShowCharacterName = this.Box_CharacterName.Checked;
            this.DiscordClient.UpdatePresence();
        }

        private void Box_ShowGroup_CheckedChanged(object sender, EventArgs e)
        {
            this.DiscordClient.ShowGroupInfo = this.Box_ShowGroup.Checked;
            this.DiscordClient.UpdatePresence();
        }
    }
}
