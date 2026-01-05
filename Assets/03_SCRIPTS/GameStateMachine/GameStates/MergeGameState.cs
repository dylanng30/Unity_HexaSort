using HexaSort.GameStateMachine.Interfaces;
using UnityEngine;

namespace HexaSort.GameStateMachine.GameStates
{
    public class MergeGameState : BaseGameState
    {
        public MergeGameState(GameManager gameManager) : base(gameManager)
        {
            
        }

        public override void Enter()
        {
            HexaCell startCell = _gameManager.LastPlacedHexaCell;
            
            if (startCell == null)
            {
                Debug.LogError("Vào MergeState nhưng không có Cell nào được đặt!");
                _gameManager.ChangeState(GameState.MAIN_PLAY);
                return;
            }
            
            _gameManager.MergeManager.ExecuteMergeSequence(startCell, OnMergeFinished);
        }
        
        private void OnMergeFinished()
        {
            var levelManager = _gameManager.LevelManager;
            
            var gameState = levelManager.GetLevelStatus();

            if (gameState == GameState.LEVEL_COMPLETED)
            {
                _gameManager.CompleteLevel();
            }
            
            if (gameState == GameState.MAIN_PLAY)
            {
                levelManager.CheckSpawnStacks();
            }
            
            //Check stack spawner
            _gameManager.LastPlacedHexaCell = null;
            _gameManager.ChangeState(gameState);
        }
    }
}