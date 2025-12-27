using System.Collections.Generic;

namespace HexaSort.SaveLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int currentUnlockedLevel;

        public PlayerData()
        {
            currentUnlockedLevel = 1;
        }
    }
}