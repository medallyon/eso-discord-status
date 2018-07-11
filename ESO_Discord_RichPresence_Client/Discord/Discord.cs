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
            this.PresenceData.startTimestamp = this.Main.StartTimestamp;

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
            if (!this.Main.EsoIsRunning || character == null)
                return;
            
            this.PresenceData.state = ((character.InDungeon) ? (Zones.Trials.IsValid(character.Zone) || Zones.Dungeons.IsValid(character.Zone) ? $"In a dungeon{((character.GroupRole != null) ? $" as {character.GroupRole}" : (character.PreferredGroupRoles.Length > 0) ? $" as {String.Join(", ", character.PreferredGroupRoles)}" : "")}" : $"Venturing through a Delve") : "Roaming Tamriel");
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

            this.PresenceData.largeImageKey = ((character.InDungeon) ? ((Zones.Trials.IsValid(character.Zone)) ? Zones.Trials.Get(character.Zone) : Zones.Dungeons.Get(character.Zone)) : Zones.Locations.Get(character.Zone));
            this.PresenceData.largeImageText = character.Zone;
            
            if (character.InDungeon && (Zones.Trials.IsValid(character.Zone) || Zones.Dungeons.IsValid(character.Zone)))
            {
                this.PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                this.PresenceData.smallImageKey = $"difficulty_{character.DungeonDifficulty.ToLower()}";
                this.PresenceData.smallImageText = $"{character.DungeonDifficulty} Mode";
            }

            else
            {
                this.PresenceData.startTimestamp = this.Main.StartTimestamp;
                this.PresenceData.smallImageKey = String.Empty;
                this.PresenceData.smallImageText = String.Empty;
            }

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
