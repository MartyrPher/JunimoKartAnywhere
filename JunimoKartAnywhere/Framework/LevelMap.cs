using StardewValley.Minigames;
using System.Collections.Generic;

namespace JunimoKartAnywhere.Framework
{
    /// <summary>Class that holds junimo kart level information</summary>
    public class LevelMap
    {
        /// <summary>Dictionary with all <see cref="MineCart"/> levels mapped to a string</summary>
        public static Dictionary<string, MineCart.LevelTransition> KartLevelMap = new Dictionary<string, MineCart.LevelTransition>();

        /// <summary>Constructor that adds the levels to <see cref="KartLevelMap"/></summary>
        public LevelMap()
        {
            KartLevelMap.Add("Crumble Cavern", new MineCart.LevelTransition(-1, 0, 2, 5, "rrr", null));
            KartLevelMap.Add("Slippery Slopes", new MineCart.LevelTransition(-1, 1, 5, 5, "rddlddrdd", null));
            KartLevelMap.Add("Slomp's Stomp", new MineCart.LevelTransition(-1, 5, 8, 8, "ddrruuu", null));
            KartLevelMap.Add("???", new MineCart.LevelTransition(-1, 8, 5, 5, "rddrrd", null));
            KartLevelMap.Add("The Gem Sea Giant", new MineCart.LevelTransition(-1, 2, 6, 11, "rrurrrrddr", null));
            KartLevelMap.Add("Ghastly Galleon", new MineCart.LevelTransition(-1, 3, 10, 7, "urruulluurrrrrddddddr", null));
            KartLevelMap.Add("Glowshroom Grotto", new MineCart.LevelTransition(-1, 9, 16, 8, "rruuluu", null));
            KartLevelMap.Add("Red Hot Rollercoaster", new MineCart.LevelTransition(-1, 4, 16, 8, "rrddrddr", null));
            KartLevelMap.Add("Sunset Speedway", new MineCart.LevelTransition(-1, 6, 17, 4, "rrdrrru", null));   
        }
    }
}
