using System.Collections.Generic;

namespace HexaSort.SaveLoadSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int Level;
        public int NormalRocket;
        public int SuperRocket;
        public int Swap;

        public PlayerData()
        {
            Level = 1;
            NormalRocket = 1;
            SuperRocket = 1;
            Swap = 1;
        }
    }
}