namespace HexaSort._01_Models
{
    public enum JellyFruit
    {
        None,
        StrawBerry,
        Apple,
    }

    public class JellyModel
    {
        public JellyFruit JellyFruit { get; private set; }

        public JellyModel(JellyFruit jellyFruit)
        {
            JellyFruit = jellyFruit;
        }
    }
}