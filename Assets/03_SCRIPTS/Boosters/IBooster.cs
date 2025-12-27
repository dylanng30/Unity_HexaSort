namespace HexaSort.Boosters
{
    public interface IBooster
    {
        void Select();
        void Apply(HexaCell target);
        void Cancel();
    }
}