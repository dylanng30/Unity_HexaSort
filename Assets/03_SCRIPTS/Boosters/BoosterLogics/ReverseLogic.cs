using System;
using System.Collections.Generic;
using HexaSort.Utilitilies;

namespace HexaSort.Boosters.BoosterLogics
{
    public class ReverseLogic : IBoostLogic
    {
        public void Execute(HexaBoard board, HexaCell cell, Action onComplete)
        {
            cell.HexaStack.Reverse();
            onComplete?.Invoke();
        }
    }
}