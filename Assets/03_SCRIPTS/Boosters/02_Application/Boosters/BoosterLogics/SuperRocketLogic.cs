using System;
using System.Collections.Generic;
using HexaSort.Utilitilies;

namespace HexaSort.Boosters.BoosterLogics
{
    public class SuperRocketLogic : IBoostLogic
    {
        public void Execute(HexaBoard board, HexaCell cell, Action onComplete)
        {
            List<HexaCell> destroyedCells = HexaAlgorithm.GetNeighborsInRadius(board, cell, 1);

            for (int i = 0; i < destroyedCells.Count; i++)
            {
                destroyedCells[i].Clear(); 
            }
        }
    }
}