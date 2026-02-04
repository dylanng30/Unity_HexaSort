namespace HexaSort._01_Models
{
    public class CellModel
    {
        public StackModel StackModel { get; private set; }
        public bool CanSetUp => StackModel == null;

        public void Place(StackModel stackModel)
        {
            StackModel = stackModel;
        }

        public void Clear()
        {
            StackModel = null;
        }
    }
}