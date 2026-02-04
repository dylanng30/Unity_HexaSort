using System.Collections.Generic;
using HexaSort.Boosters;
using HexaSort.Boosters.Data;

namespace HexaSort
{
    public static class GameContext
    {
        public static int CurrentLevel;
        public static Dictionary<BoosterType, int> BoosterInventory = new Dictionary<BoosterType, int>();
    }
}