using HexaSort.GameStateMachine.Interfaces;

namespace HexaSort.GameStateMachine.GameStates
{
    public class LogoGameState : BaseGameState
    {
        private GameManager _gameManager;
        public LogoGameState(GameManager gameManager) : base(gameManager)
        {
            
        }
    }
}