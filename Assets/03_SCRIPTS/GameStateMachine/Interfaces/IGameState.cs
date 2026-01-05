namespace HexaSort.GameStateMachine.Interfaces
{
    public interface IGameState
    {
        void Enter();
        void Update();
        void HandleInput();
        void Exit();
    }
}