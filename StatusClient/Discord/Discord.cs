using System;

namespace ESO_Discord_RichPresence_Client
{
    internal class Discord : DiscordRpc
    {
        public static EsoCharacter CurrentCharacter;
        private EsoCharacter _latestInstance;

        private string ApplicationID { get; set; }
        private string OptionalSteamAppID { get; set; }
        private int CallbackCalls { get; set; }

        private bool ShowCharacterName => (bool)Main.Settings.Get("ShowCharacterName");
        private bool ShowPartyInfo => (bool)Main.Settings.Get("ShowPartyInfo");

        internal Main Main;
        public RichPresence PresenceData = new RichPresence();
        private EventHandlers _handlers;

        public Discord(Main form, string appID)
        {
            Main = form;
            ApplicationID = appID;

            _handlers = new EventHandlers
            {
                readyCallback = OnReady,
                errorCallback = OnError
            };
        }

        public Discord(Main form, string appID, string steamAppID)
        {
            Main = form;
            ApplicationID = appID;
            OptionalSteamAppID = steamAppID;

            _handlers = new EventHandlers
            {
                readyCallback = OnReady,
                errorCallback = OnError
            };
        }

        public void Enable()
        {
            if (!Main.EsoIsRunning)
                return;

            CallbackCalls = 0;
            PresenceData.startTimestamp = Main.StartTimestamp;

            Initialize(ApplicationID, ref _handlers, false, OptionalSteamAppID);
        }

        public void UpdatePresence()
        {
            if (!Main.EsoIsRunning)
                return;

            UpdatePresence(CurrentCharacter);
        }

        public void UpdatePresence(EsoCharacter character)
        {
            if (!Main.EsoIsRunning || character == null)
                return;

            // Character | Account (Level|CP)
            PresenceData.details = $"{((ShowCharacterName) ? character.Name : character.Account)} ({((character.IsChampion) ? "CP" : "Level ")}{character.Level})";

            // State + Image + Time Data

            PresenceData.state = character.QuestName;

            PresenceData.largeImageKey = ImageKeys.Locations.Get(character.Zone);

            // Try to get image for parent zone if not found; will probably still return default
            if (PresenceData.largeImageKey == "default")
                PresenceData.largeImageKey = ImageKeys.Locations.Get(character.ParentZone);

            PresenceData.largeImageText = (character.SubZone != null && character.SubZone.Length > 0) ? character.SubZone : character.Zone;
            PresenceData.smallImageKey = $"class_{character.Class.ToLower()}";
            PresenceData.smallImageText = character.Class;

            PresenceData.startTimestamp = Main.StartTimestamp;

            if (character.InDungeon)
            {
                PresenceData.largeImageText = character.Dungeon;

                if (ImageKeys.Trials.IsValid(character.Dungeon) || ImageKeys.Dungeons.IsValid(character.Dungeon))
                {
                    PresenceData.state = (character.GroupRole != null) ? character.GroupRole : "";

                    // Don't update timestamp if in same dungeon instance
                    if (_latestInstance != null
                        && !(_latestInstance.InDungeon
                            && _latestInstance.Dungeon == character.Dungeon
                            && _latestInstance.DungeonDifficulty == character.DungeonDifficulty
                        )
                    )
                        PresenceData.startTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                    PresenceData.largeImageKey = (ImageKeys.Trials.IsValid(character.Dungeon)) ? ImageKeys.Trials.Get(character.Dungeon) : ImageKeys.Dungeons.Get(character.Dungeon);
                    PresenceData.smallImageKey = $"difficulty_{character.DungeonDifficulty.ToLower()}";
                    PresenceData.smallImageText = $"{character.DungeonDifficulty} Mode";
                }
            }

            // Party Data

            PresenceData.partyMax = 0;
            PresenceData.partySize = 0;

            if (ShowPartyInfo)
            {
                PresenceData.partyMax = ((character.GroupSize <= 4) ? 4 : ((character.GroupSize <= 12) ? 12 : 24));
                PresenceData.partySize = character.GroupSize;
            }

            DiscordRpc.UpdatePresence(PresenceData);
            _latestInstance = character;
        }

        private void OnReady(ref DiscordUser connectedUser)
        {
            CallbackCalls++;
            Console.WriteLine($"Discord: connected to {connectedUser.username}#{connectedUser.discriminator} (ID {connectedUser.userId})");
        }

        private void OnError(int errorCode, string message)
        {
            CallbackCalls++;
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
