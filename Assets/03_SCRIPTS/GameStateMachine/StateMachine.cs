using HexaSort.GameStateMachine.Interfaces;

namespace HexaSort.GameStateMachine
{
    public class StateMachine
    {
        public IGameState CurrentState { get; private set; }

        public void ChangeState(IGameState newState)
        {
            if (CurrentState == newState) return;
            
            if (CurrentState != null)
                CurrentState.Exit();
            
            CurrentState = newState;
            
            if (CurrentState != null)
                CurrentState.Enter();
        }

        public void Update()
        {
            if (CurrentState != null)
                CurrentState.Update();
        }

        public void HandleInput()
        {
            if (CurrentState != null)
                CurrentState.HandleInput();
        }
    }
}