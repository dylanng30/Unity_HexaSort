using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace HexaSort._01_Models
{
    public class BoardModel
    {
        public Dictionary<Vector3Int, CellModel> Cells = new ();
    }
}