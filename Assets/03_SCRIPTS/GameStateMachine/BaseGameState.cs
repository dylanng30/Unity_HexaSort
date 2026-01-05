using HexaSort.GameStateMachine.Interfaces;

namespace HexaSort.GameStateMachine
{
    public abstract class BaseGameState : IGameState
    {
        protected GameManager _gameManager;

        public BaseGameState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public virtual void Enter(){}
        public virtual void Update(){}
        public virtual void HandleInput(){}
        public virtual void Exit() {}
    }
}