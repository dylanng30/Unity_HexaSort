using System.Collections.Generic;
using HexaSort.Boosters;

namespace HexaSort
{
    public static class GameContext
    {
        public static int CurrentLevel;
        public static Dictionary<BoosterType, int> BoosterInventory = new Dictionary<BoosterType, int>();
    }
}