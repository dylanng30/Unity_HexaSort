using HexaSort._04_Services.BoardModelGenerateService.Interfaces;

namespace HexaSort._01_Models
{
    public class LevelData
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IBoardGenerator BoardGenerator { get; private set; }
    }
}