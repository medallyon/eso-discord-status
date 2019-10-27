using System;
using System.Collections.Generic;
using System.Linq;
using NLua;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    class EsoCharacter
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }

        private int genderInt;
        public string Gender
        {
            get
            {
                switch (this.genderInt)
                {
                    case 1:
                        return "Female";
                    default:
                        return "Male";
                }
            }
        }

        private int allianceInt;
        public string Alliance
        {
            get
            {
                switch (this.allianceInt)
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
        public string Zone { get; set; }

        public bool IsChampion { get; set; }
        public int Level { get; set; }
        
        public bool IsGrouped { get; set; }
        public int GroupSize { get; set; }

        private int groupRoleInt;
        public string GroupRole
        {
            get
            {
                switch (this.groupRoleInt)
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

        public string[] PreferredGroupRoles
        {
            get
            {
                var preferred = new Dictionary<string, bool>
                {
                    { "DPS", this.PrefersDPS },
                    { "Tank", this.PrefersTank },
                    { "Healer", this.PrefersHeal }
                };

                foreach (var role in preferred.Where(r => !r.Value).ToList())
                    preferred.Remove(role.Key);

                return preferred.Keys.ToArray();
            }
        }

        public bool InDungeon { get; set; }

        private int dungeonDifficultyInt;
        public string DungeonDifficulty
        {
            get
            {
                switch (this.dungeonDifficultyInt)
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
        public bool PrefersDPS { get; set; }
        public bool PrefersTank { get; set; }
        public bool PrefersHeal { get; set; }

        public int Battlegrounds_GameType { get; set; }
        public string Battlegrounds_Name { get; set; }
        public string Battlegrounds_Description { get; set; }

        public string QuestName { get; set; }

        public EsoCharacter(LuaTable character)
        {
            this.Name = (string)character["name"];
            this.Account = (string)character["account"];
            this.Race = (string)character["race"];
            this.Class = (string)character["class"];
            this.genderInt = (int)(double)character["gender"];
            this.allianceInt = (int)(double)character["alliance"];
            this.Zone = (string)character["zone"];
            this.IsChampion = (bool)character["isChampion"];
            this.Level = (int)(double)character["level"];

            this.IsGrouped = (bool)character["isGrouped"];
            this.GroupSize = (int)(double)character["groupSize"];
            this.groupRoleInt = (int)(double)character["groupRole"];
            this.InDungeon = (bool)character["inDungeon"];
            this.dungeonDifficultyInt = (int)(double)character["isDungeonVeteran"];
            this.PrefersDPS = (bool)character["prefersDPS"];
            this.PrefersTank = (bool)character["prefersTank"];
            this.PrefersHeal = (bool)character["prefersHeal"];

            this.Battlegrounds_GameType = (int)(double)character["bg_GameType"];
            this.Battlegrounds_Name = (string)character["bg_Name"];
            this.Battlegrounds_Description = (string)character["bg_Description"];

            this.QuestName = (string)character["activeQuest"];
        }
    }
}
