using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaSort.Utilitilies
{
    public class Constants : MonoBehaviour
    {
        public const float HeightHexaModel = 0.25f;
        public const float CellSize = 1f;
        public const string GridLayer = "Grid";

        public static readonly Vector3[] HexaDirections = new Vector3[6]
        {
            new Vector3(1, 0, -1),// East
            new Vector3(1, -1, 0),// North-East
            new Vector3(0, -1, 1),// North-West
            new Vector3(-1, 0, 1),// West
            new Vector3(-1, 1, 0),// South-West
            new Vector3(0, 1, -1)// South-East
        };
    }
}

