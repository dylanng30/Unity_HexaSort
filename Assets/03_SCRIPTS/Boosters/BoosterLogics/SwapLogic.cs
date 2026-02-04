using System;
using System.Collections.Generic;
using HexaSort.Utilities;

namespace HexaSort.Boosters.BoosterLogics
{
    public class SwapLogic : IBoostLogic
    {
        public void Execute(HexaBoard board, HexaCell cell, Action onComplete)
        {
            cell.Clear();
        }
    }
}