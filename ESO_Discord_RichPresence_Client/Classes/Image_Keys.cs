using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    public static class Image_Keys
    {
        #region Data
        private static Dictionary<string, string[]> ZoneMap = new Dictionary<string, string[]>(3)
        {
            {
                "zone",
                new string[]
                {
                    "Alik'r Desert",
                    "Artaeum",
                    "Auridon",
                    "Bal Foyen",
                    "Bangkorai",
                    "Betnikh",
                    "Bleakrock Isle",
                    "The Clockwork City",
                    "Coldharbour",
                    "Craglorn",
                    "Cyrodiil",
                    "Deshaan",
                    "Eastmarch",
                    "Glenumbra",
                    "Gold Coast",
                    "Grahtwood",
                    "Greenshade",
                    "Hew's Bane",
                    "Imperial City",
                    "Khenarthi's Roost",
                    "Malabal Tor",
                    "Murkmire",
                    "Norg-Tzel",
                    "Northern Elsweyr",
                    "Reaper's March",
                    "Rivenspire",
                    "Shadowfen",
                    "Southern Elsweyr",
                    "Stonefalls",
                    "Stormhaven",
                    "Stros M'Kai",
                    "Summerset",
                    "The Rift",
                    "Vvardenfell",
                    "Wrothgar"
                }
            },
            {
                "dungeon",
                new string[]
                {
                    "Arx Corinium",
                    "Blackheart Haven",
                    "Blackrose Prison",
                    "Blessed Crucible",
                    "Bloodroot Forge",
                    "City of Ash I",
                    "City of Ash II",
                    "Cradle of Shadows",
                    "Crypt of Hearts I",
                    "Crypt of Hearts II",
                    "Darkshade Caverns I",
                    "Darkshade Caverns II",
                    "Depths of Malatar",
                    "Direfrost Keep",
                    "Dragonstar Arena",
                    "Elden Hollow I",
                    "Elden Hollow II",
                    "Falkreath Hold",
                    "Fang Lair",
                    "Frostvault",
                    "Fungal Grotto I",
                    "Fungal Grotto II",
                    "Imperial City Prison",
                    "Lair of Maarselok",
                    "Maelstrom Arena",
                    "March of Sacrifices",
                    "Moon Hunter Keep",
                    "Moongrave Fane",
                    "Ruins of Mazzatun",
                    "Scalecaller Peak",
                    "Selene's Web",
                    "Spindleclutch I",
                    "Spindleclutch II",
                    "Tempest Island",
                    "The Banished Cells I",
                    "The Banished Cells II",
                    "Vaults of Madness",
                    "Wayrest Sewers I",
                    "Wayrest Sewers II",
                    "White-Gold Tower"
                }
            },
            {
                "trial",
                new string[]
                {
                    "Asylum Sanctorium",
                    "Aetherian Archive",
                    "Cloudrest",
                    "Halls of Fabrication",
                    "Hel Ra Citadel",
                    "Maw of Lorkhaj",
                    "Sanctum Ophidia",
                    "Sunspire"
                }
            }
        };
        #endregion

        #region Private methods
        private static bool Lookup(string category, string locationName)
        {
            if (!ZoneMap.ContainsKey(category))
                return false;

            return ZoneMap[category].Any(l => l.ToLowerInvariant() == locationName.ToLowerInvariant());
        }

        private static string GetKey(string category, string locationName)
        {
            if (!Lookup(category, locationName))
                return "default";

            return $"{category}_" + locationName.ToLower()
                .Replace(' ', '_')
                .Replace('-', '_')
                .Replace("\'", String.Empty);
        }
        #endregion

        #region Specialisation classes for zone types
        public static class Locations
        {
            public static bool IsValid(string locationName)
            {
                return Lookup("zone", locationName);
            }

            public static string Get(string locationName)
            {
                return GetKey("zone", locationName);
            }
        }

        public static class Dungeons
        {
            public static bool IsValid(string locationName)
            {
                return Lookup("dungeon", locationName);
            }

            public static string Get(string locationName)
            {
                return GetKey("dungeon", locationName);
            }
        }

        public static class Trials
        {
            public static bool IsValid(string locationName)
            {
                return Lookup("trial", locationName);
            }

            public static string Get(string locationName)
            {
                return GetKey("trial", locationName);
            }
        }
        #endregion
    }
}
