using HexaSort._01_Models;

namespace HexaSort._04_Services.BoardModelGenerateService.Interfaces
{
    public interface IBoardGenerator
    {
        void Generate(BoardModel boardModel, int width, int height);
    }
}