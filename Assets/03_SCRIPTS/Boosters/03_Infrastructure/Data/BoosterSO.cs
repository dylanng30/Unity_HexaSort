using UnityEngine;

namespace HexaSort.Boosters.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/BoosterSO")]
    public class BoosterSO : ScriptableObject
    {
        public string BoosterName;
        public string BoosterDescription;
        public BoosterType BoosterType;
        public Sprite BoosterIcon;
        public int price;
    }
}