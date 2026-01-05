namespace HexaSort.Boosters.BoosterLogics
{
    public interface IBoostLogic
    {
        void Execute(HexaBoard board, HexaCell cell, System.Action onComplete);
    }
}