using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.Utilitilies
{
    public class Constants : MonoBehaviour
    {
        public const float HeightHexaModel = 0.25f;
        public const float CellSize = 1f;

        public const int MergeThreshold = 10;
        
        
        public const string GridLayer = "Grid";
        public const string HexagonLayer = "Hexagon";
        public const string GroundLayer = "Ground";

        public static readonly Vector3Int[] HexaDirections = new Vector3Int[6]
        {
            new Vector3Int(1, 0, -1), // East
            new Vector3Int(1, -1, 0), // North-East
            new Vector3Int(0, -1, 1), // North-West
            new Vector3Int(-1, 0, 1), // West
            new Vector3Int(-1, 1, 0), // South-West
            new Vector3Int(0, 1, -1)  // South-East
        };
        
        
        public const string BoosterDataPath = "BoosterData";
        
    }
}

