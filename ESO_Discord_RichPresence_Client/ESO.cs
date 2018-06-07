using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    public class ZoneContainer
    {
        internal string type;
        internal string[] Names;

        private string ResolveKeyname(string name)
        {
            if (!this.Names.Any(x => x.ToLower() == name.ToLower()))
                return "default";

            return $"{this.type}_" + name.ToLower()
                .Replace(' ', '_')
                .Replace('-', '_')
                .Replace("\'", String.Empty);
        }

        public string this[string index]
        {
            get
            {
                return ResolveKeyname(index);
            }
        }

        public bool Contains(string zone)
        {
            return (this[zone] == "default");
        }
    }

    public class Dungeons : ZoneContainer
    {
        static private string key_type = "dungeon";

        public Dungeons()
        {
            this.type = key_type;
            this.Names = new string[]
            {
                "Arx Corinium",
                "Blackheart Haven",
                "Blessed Crucible",
                "Bloodroot Forge",
                "City of Ash I",
                "City of Ash II",
                "Cradle of Shadows",
                "Crypt of Hearts I",
                "Crypt of Hearts II",
                "Darkshade Caverns I",
                "Darkshade Caverns II",
                "Direfrost Keep",
                "Elden Hollow I",
                "Elden Hollow II",
                "Falkreath Hold",
                "Fang Lair",
                "Fungal Grotto I",
                "Fungal Grotto II",
                "Imperial City Prison",
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
            };
        }
    }

    public class Zones : ZoneContainer
    {
        private static string key_type = "zone";

        public Zones()
        {
            this.type = key_type;
            this.Names = new string[]
            {
                "Auridon",
                "Grahtwood",
                "Greenshade",
                "Khenarthi's Roost",
                "Malabal Tor",
                "Reaper's March",
                "Alik'r Desert",
                "Bangkorai",
                "Betnikh",
                "Glenumbra",
                "Rivenspire",
                "Stormhaven",
                "Stros M'Kai",
                "Bal Foyen",
                "Bleakrock Isle",
                "Deshaan",
                "Eastmarch",
                "The Rift",
                "Shadowfen",
                "Stonefalls",
                "Artaeum",
                "Clockwork City",
                "Coldharbour",
                "Craglorn",
                "Cyrodiil",
                "Gold Coast",
                "Hew's Bane",
                "Summerset",
                "Vvardenfell",
                "Wrothgar"
            };
        }
    }

    public class ESO
    {
        public static Dungeons Dungeons = new Dungeons();
        public static Zones Zones = new Zones();
    }
}
