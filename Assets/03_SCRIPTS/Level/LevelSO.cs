using UnityEngine;

namespace HexaSort.Level
{
    [CreateAssetMenu(fileName = "New Level", menuName = "HexaSort Level")]
    public class LevelSO : ScriptableObject
    {
        public int LevelId;
        public int GridWidth;
        public int GridHeight;
        public int TargetGoal;
        public int MergeCount;
        public int MiniumHexaAmount;
        public int MaxiumHexaAmount;
        public float TimeLimit;
        public int MoveLimit;
        public Material[] Materials;
    }
}