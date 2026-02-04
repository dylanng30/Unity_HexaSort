using System.Collections.Generic;

namespace HexaSort._03_Controllers
{
    public enum GameState
    {
        MENU, PLAY
    }
    
    public class GameStateController
    {
        private GameState _currentState;
        private Dictionary<GameState, int> _states;
        public GameStateController()
        {
            LoadGameStates();
            ChangeState(GameState.MENU);
        }

        public void ChangeState(GameState newState)
        {
            _currentState = newState;
        }

        private void LoadGameStates()
        {
            
        }
    }
}