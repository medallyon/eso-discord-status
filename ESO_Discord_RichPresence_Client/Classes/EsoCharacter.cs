using NLua;

namespace ESO_Discord_RichPresence_Client
{
    internal class EsoCharacter
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }

        private readonly int _genderInt;
        public string Gender => _genderInt == 1 ? "Female" : "Male";

        private readonly int _allianceInt;
        public string Alliance
        {
            get
            {
                switch (_allianceInt)
                {
                    case 1:
                        return "Aldmeri Dominion";
                    case 2:
                        return "Ebonheart Pact";
                    default:
                        return "Daggerfall Covenant";
                }
            }
        }
        public string ParentZone { get; set; }
        public string Zone { get; set; }
        public string SubZone { get; set; }

        public bool IsChampion { get; set; }
        public int Level { get; set; }
        
        public bool IsGrouped { get; set; }
        public int GroupSize { get; set; }

        private readonly int _groupRoleInt;
        public string GroupRole
        {
            get
            {
                switch (_groupRoleInt)
                {
                    case 1:
                        return "DPS";
                    case 2:
                        return "Tank";
                    case 4:
                        return "Healer";
                    default:
                        return null;
                }
            }
        }

        public bool InDungeon { get; set; }
        public string Dungeon { get; set; }

        private readonly int _dungeonDifficultyInt;
        public string DungeonDifficulty
        {
            get
            {
                switch (_dungeonDifficultyInt)
                {
                    case 1:
                        return "Normal";
                    case 2:
                        return "Veteran";
                    default:
                        return "";
                }
            }
        }

        public int BattlegroundsGameType { get; set; }
        public string BattlegroundsName { get; set; }
        public string BattlegroundsDescription { get; set; }

        public string QuestName { get; set; }

        public EsoCharacter(LuaTable character)
        {
            Name = (string)character["name"];
            Account = (string)character["account"];
            Race = (string)character["race"];
            Class = (string)character["class"];
            _genderInt = (int)(long)character["gender"];
            _allianceInt = (int)(long)character["alliance"];
            ParentZone = (string)character["parentZone"];
            Zone = (string)character["zone"];
            SubZone = (string)character["subZone"];
            IsChampion = (bool)character["isChampion"];
            Level = (int)(long)character["level"];

            IsGrouped = (bool)character["isGrouped"];
            GroupSize = (int)(long)character["groupSize"];
            _groupRoleInt = (int)(long)character["groupRole"];
            InDungeon = (bool)character["inDungeon"];
            _dungeonDifficultyInt = (int)(long)character["isDungeonVeteran"];

            BattlegroundsGameType = (int)(long)character["bg_GameType"];
            BattlegroundsName = (string)character["bg_Name"];
            BattlegroundsDescription = (string)character["bg_Description"];

            QuestName = (string)character["activeQuest"];

            if (InDungeon)
            {
                Dungeon = Zone;
                Zone = (string)character["parentZone"];
            }
        }
    }
}
