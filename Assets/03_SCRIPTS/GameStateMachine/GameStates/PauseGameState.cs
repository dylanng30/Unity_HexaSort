using HexaSort.GameStateMachine.Interfaces;

namespace HexaSort.GameStateMachine.GameStates
{
    public class PauseGameState : BaseGameState
    {
        public PauseGameState(GameManager gameManager) : base(gameManager)
        {
            _gameManager = gameManager;
        }
    }
}