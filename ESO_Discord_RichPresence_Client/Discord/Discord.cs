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

        public bool Enabled;
        public bool ShowCharacterName;
        public bool ShowGroupInfo;

        private string ApplicationID { get; set; }
        private string OptionalSteamAppID { get; set; }
        private int CallbackCalls { get; set; }

        public RichPresence PresenceData = new RichPresence();
        private EventHandlers handlers;

        public Discord(string appID)
        {
            this.ApplicationID = appID;

            this.handlers = new EventHandlers
            {
                readyCallback = this.OnReady,
                errorCallback = this.OnError
            };
        }

        public Discord(string appID, string steamAppID)
        {
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
            Console.WriteLine("Discord: init");
            this.CallbackCalls = 0;
            this.PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Initialize(this.ApplicationID, ref this.handlers, false, this.OptionalSteamAppID);
        }

        public void UpdatePresence()
        {
            this.UpdatePresence(Discord.CurrentCharacter);
        }

        public new void UpdatePresence(RichPresence data)
        {
            DiscordRpc.UpdatePresence(data);
        }

        public void UpdatePresence(EsoCharacter character)
        {
            this.PresenceData.state = ((character.InDungeon) ? "In a dungeon" : "Roaming Tamriel");
            this.PresenceData.details = $"{((this.ShowCharacterName) ? character.Name : character.Account)} ({((character.IsChampion) ? "CP" : "Level ")}{character.Level})";

            if (this.ShowGroupInfo)
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
            Console.WriteLine("Discord: shutdown");
            Shutdown();
        }
    }
}
