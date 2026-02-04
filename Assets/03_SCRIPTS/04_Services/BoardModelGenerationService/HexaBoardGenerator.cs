using HexaSort._01_Models;
using HexaSort._04_Services.BoardModelGenerateService.Interfaces;
using UnityEngine;

namespace HexaSort._04_Services.BoardModelGenerateService
{
    public class HexaBoardGenerator : IBoardGenerator
    {
        public void Generate(BoardModel boardModel, int width, int height)
        {
            for (int q = -width; q <= width; q++)
            {
                int r1 = Mathf.Max(-width, -q - width);
                int r2 = Mathf.Min(width, -q + width);
            
                for (int r = r1; r <= r2; r++)
                {
                    int s = -q - r;
                    
                    CellModel cellModel = new CellModel();
                    boardModel.Cells.Add(new Vector3Int(q, r, s), cellModel);
                }
            }
        }
    }
}