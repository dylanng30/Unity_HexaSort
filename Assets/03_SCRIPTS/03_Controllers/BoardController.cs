using System;
using HexaSort._01_Models;
using HexaSort._02_Views;
using HexaSort._04_Services.BoardModelGenerateService;
using HexaSort._04_Services.BoardModelGenerateService.Interfaces;
using UnityEngine;

namespace HexaSort._03_Controllers
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardView _boardView;
        
        private BoardModel _boardModel;
        
        private IBoardModelGenerationService _boardModelGenerationService;

        private void Start()
        {
            _boardModelGenerationService = new BoardModelGenerationService();
        }

        public void Initialize(
            IBoardModelGenerationService boardModelGenerationService)
        {
            
        }

        public void StartNewGame(LevelData levelData)
        {
            _boardModelGenerationService.SetupMapGenerator(levelData.BoardGenerator);
            _boardModel = _boardModelGenerationService.GetBoardModel(levelData.Width, levelData.Height);
            
            GenerateBoard();
        }

        public void GenerateBoard()
        {
            if(_boardModel == null) return;
            _boardView.GenerateBoard(_boardModel);
        }
    }
}