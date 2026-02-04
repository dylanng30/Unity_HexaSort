using HexaSort._01_Models;
using HexaSort._04_Services.BoardModelGenerateService.Interfaces;
using UnityEngine;

namespace HexaSort._04_Services.BoardModelGenerateService
{
    public class BoardModelGenerationService : IBoardModelGenerationService
    {
        private IBoardGenerator _boardGenerator;
        
        public void SetupMapGenerator(IBoardGenerator boardGenerator)
        {
            _boardGenerator = boardGenerator;
        }
        public BoardModel GetBoardModel(int width, int height)
        {
            BoardModel boardModel = new  BoardModel();
            _boardGenerator.Generate(boardModel, width, height);
            return boardModel;
        }
    }
}