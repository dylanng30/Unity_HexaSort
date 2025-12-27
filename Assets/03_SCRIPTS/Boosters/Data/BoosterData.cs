using UnityEngine;

namespace HexaSort.Boosters.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/BoosterData")]
    public class BoosterData : ScriptableObject
    {
        public string BoosterName;
        public string BoosterDescription;
        public BoosterType BoosterType;
        public Sprite BoosterIcon;
        public int price;
    }
}