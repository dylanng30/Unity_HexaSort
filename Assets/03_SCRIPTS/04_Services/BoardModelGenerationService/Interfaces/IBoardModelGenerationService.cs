using HexaSort._01_Models;

namespace HexaSort._04_Services.BoardModelGenerateService.Interfaces
{
    public interface IBoardModelGenerationService
    {
        void SetupMapGenerator(IBoardGenerator boardGenerator);
        BoardModel GetBoardModel(int width, int height);
    }
}