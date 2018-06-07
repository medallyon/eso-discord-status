using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    class Discord : DiscordRpc
    {
        private string ApplicationID { get; set; }
        private string OptionalSteamAppID { get; set; }
        private int CallbackCalls { get; set; }

        public RichPresence PresenceData = new RichPresence();
        private EventHandlers handlers;

        public Discord(string appID, bool initialise)
        {
            this.ApplicationID = appID;

            if (initialise)
                this.Enable();
        }

        public Discord(string appID, string steamAppID, bool initialise)
        {
            this.ApplicationID = appID;
            this.OptionalSteamAppID = steamAppID;

            if (initialise)
                this.Enable();
        }

        public void Enable()
        {
            Console.WriteLine("Discord: init");
            this.CallbackCalls = 0;

            this.handlers = new EventHandlers
            {
                readyCallback = this.OnReady,
                errorCallback = this.OnError
            };

            this.PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Initialize(this.ApplicationID, ref this.handlers, false, this.OptionalSteamAppID);
        }

        public void UpdatePresence()
        {
            DiscordRpc.UpdatePresence(this.PresenceData);
        }

        public new void UpdatePresence(RichPresence data)
        {
            DiscordRpc.UpdatePresence(data);
        }

        public void UpdatePresence(EsoCharacter character)
        {
            this.PresenceData.state = ((character.InDungeon) ? "In a dungeon" : "Roaming Tamriel");
            this.PresenceData.details = $"{character.Name} ({((character.IsChampion) ? "CP" : "Level ")}{character.Level})";

            this.PresenceData.partyMax = ((character.GroupSize <= 4) ? 4 : ((character.GroupSize <= 12) ? 12 : 24));
            this.PresenceData.partySize = character.GroupSize;

            this.PresenceData.largeImageKey = ((character.InDungeon) ? ESO.Dungeons[character.Zone] : "default");
            this.PresenceData.largeImageText = character.Zone;

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

        private void Disable()
        {
            Console.WriteLine("Discord: shutdown");
            Shutdown();
        }

        private void Destroy()
        {

        }
    }
}
