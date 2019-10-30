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
        private EsoCharacter _latestInstance;

        private string ApplicationID { get; set; }
        private string OptionalSteamAppID { get; set; }
        private int CallbackCalls { get; set; }

        private bool Enabled
        {
            get
            {
                return (bool)this.Main.Settings.Get("Enabled");
            }
        }
        private bool ShowCharacterName
        {
            get
            {
                return (bool)this.Main.Settings.Get("ShowCharacterName");
            }
        }
        private bool ShowPartyInfo
        {
            get
            {
                return (bool)this.Main.Settings.Get("ShowPartyInfo");
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

            // Character | Account (Level|CP)
            this.PresenceData.details = $"{((this.ShowCharacterName) ? character.Name : character.Account)} ({((character.IsChampion) ? "CP" : "Level ")}{character.Level})";

            // State + Image + Time Data

            this.PresenceData.state = character.QuestName;

            this.PresenceData.largeImageKey = Image_Keys.Locations.Get(character.Zone);
            this.PresenceData.largeImageText = character.Zone;
            this.PresenceData.smallImageKey = $"class_{character.Class.ToLower()}";
            this.PresenceData.smallImageText = character.Class;

            this.PresenceData.startTimestamp = this.Main.StartTimestamp;

            if (character.InDungeon)
            {
                this.PresenceData.largeImageText = character.Dungeon;

                if (Image_Keys.Trials.IsValid(character.Dungeon) || Image_Keys.Dungeons.IsValid(character.Dungeon))
                {
                    this.PresenceData.state = $"In a dungeon{((character.GroupRole != null) ? $" as a {character.GroupRole}" : "")}";

                    // Don't update timestamp if in same dungeon instance
                    if (this._latestInstance != null
                        && !(this._latestInstance.InDungeon
                            && this._latestInstance.Dungeon == character.Dungeon
                            && this._latestInstance.DungeonDifficulty == character.DungeonDifficulty
                        )
                    )
                        this.PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                    this.PresenceData.largeImageKey = (Image_Keys.Trials.IsValid(character.Dungeon)) ? Image_Keys.Trials.Get(character.Dungeon) : Image_Keys.Dungeons.Get(character.Dungeon);
                    this.PresenceData.smallImageKey = $"difficulty_{character.DungeonDifficulty.ToLower()}";
                    this.PresenceData.smallImageText = $"{character.DungeonDifficulty} Mode";
                }
            }

            // Party Data

            this.PresenceData.partyMax = 0;
            this.PresenceData.partySize = 0;

            if (this.ShowPartyInfo)
            {
                this.PresenceData.partyMax = ((character.GroupSize <= 4) ? 4 : ((character.GroupSize <= 12) ? 12 : 24));
                this.PresenceData.partySize = character.GroupSize;
            }

            DiscordRpc.UpdatePresence(this.PresenceData);
            this._latestInstance = character;
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
