using System;
using System.Collections.Generic;
using HexaSort.Utilitilies;

namespace HexaSort.Boosters.BoosterLogics
{
    public class SwapLogic : IBoostLogic
    {
        public void Execute(HexaBoard board, HexaCell cell, Action onComplete)
        {
            cell.Clear();
            
            onComplete.Invoke();
        }
    }
}