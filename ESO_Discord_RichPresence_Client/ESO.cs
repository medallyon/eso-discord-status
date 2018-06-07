using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESO_Discord_RichPresence_Client
{
    public class Dungeons
    {
        public static string[] Names = new string[]
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

        private static string resolve_keyname(string name)
        {
            if (!Names.Any(x => x.ToLower() == name.ToLower()))
                return "default";

            return "dungeon_" + name.ToLower()
                .Replace(' ', '_')
                .Replace('-', '_')
                .Replace("\'", String.Empty);
        }

        public string this[string name]
        {
            get
            {
                return resolve_keyname(name);
            }
        }
    }

    public class ESO
    {
        public static Dungeons Dungeons = new Dungeons();
    }
}
