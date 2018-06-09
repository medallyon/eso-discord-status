using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    class Discord : DiscordRpc
    {
        public static EsoCharacter CurrentCharacter;

        private string ApplicationID { get; set; }
        private string OptionalSteamAppID { get; set; }
        private int CallbackCalls { get; set; }

        private bool Enabled
        {
            get
            {
                return this.Main.Settings.Enabled;
            }
        }
        private bool ShowCharacterName
        {
            get
            {
                return this.Main.Settings.ShowCharacterName;
            }
        }
        private bool ShowPartyInfo
        {
            get
            {
                return this.Main.Settings.ShowPartyInfo;
            }
        }

        internal Main Main;
        public RichPresence PresenceData = new RichPresence();
        private EventHandlers handlers;

        public Discord(Main form, string appID)
        {
            this.Main = form;
            this.ApplicationID = appID;

            this.handlers = new EventHandlers
            {
                readyCallback = this.OnReady,
                errorCallback = this.OnError
            };
        }

        public Discord(Main form, string appID, string steamAppID)
        {
            this.Main = form;
            this.ApplicationID = appID;
            this.OptionalSteamAppID = steamAppID;

            this.handlers = new EventHandlers
            {
                readyCallback = this.OnReady,
                errorCallback = this.OnError
            };
        }

        public void Enable()
        {
            if (!this.Main.EsoIsRunning)
                return;

            this.CallbackCalls = 0;
            this.PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Initialize(this.ApplicationID, ref this.handlers, false, this.OptionalSteamAppID);
        }

        public void UpdatePresence()
        {
            if (!this.Main.EsoIsRunning)
                return;

            this.UpdatePresence(Discord.CurrentCharacter);
        }

        public new void UpdatePresence(RichPresence data)
        {
            if (!this.Main.EsoIsRunning)
                return;

            DiscordRpc.UpdatePresence(data);
        }

        public void UpdatePresence(EsoCharacter character)
        {
            if (!this.Main.EsoIsRunning)
                return;
            
            this.PresenceData.state = ((character.InDungeon) ? $"In a dungeon as {character.GroupRole}" : "Roaming Tamriel");
            this.PresenceData.details = $"{((this.ShowCharacterName) ? character.Name : character.Account)} ({((character.IsChampion) ? "CP" : "Level ")}{character.Level})";

            if (this.ShowPartyInfo)
            {
                this.PresenceData.partyMax = ((character.GroupSize <= 4) ? 4 : ((character.GroupSize <= 12) ? 12 : 24));
                this.PresenceData.partySize = character.GroupSize;
            }

            else
            {
                this.PresenceData.partyMax = 0;
                this.PresenceData.partySize = 0;
            }

            this.PresenceData.largeImageKey = ((character.InDungeon) ? ESO.Dungeons[character.Zone] : ESO.Zones[character.Zone]);
            this.PresenceData.largeImageText = ((ESO.Dungeons.Contains(character.Zone) || ESO.Zones.Contains(character.Zone)) ? character.Zone : "Tamriel");

            DiscordRpc.UpdatePresence(this.PresenceData);
        }

        private void OnReady(ref DiscordUser connectedUser)
        {
            this.CallbackCalls++;
            Console.WriteLine($"Discord: connected to {connectedUser.username}#{connectedUser.discriminator} (ID {connectedUser.userId})");
        }

        private void OnError(int errorCode, string message)
        {
            this.CallbackCalls++;
            Console.WriteLine($"Discord: error {errorCode}: {message}");
        }

        private void Update()
        {
            RunCallbacks();
        }

        public void Disable()
        {
            Shutdown();
        }
    }
}
