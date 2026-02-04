using System;

namespace HexaSort.Boosters.BoosterLogics
{
    public class NormalRocketLogic : IBoostLogic
    {
        public void Execute(HexaBoard board, HexaCell cell, Action onComplete)
        {
            cell.Clear();
            
            onComplete.Invoke();
        }
    }
}