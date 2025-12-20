using System;
using HexaSort.Level;
using HexaSort.UI;
using UnityEngine;

namespace HexaSort
{
    public enum GameState
    {
        MAIN_MENU,
        LEVEL_MENU,
        PLAYING,
        PAUSE,
        GAME_OVER
    }
    public class GameManager : MonoBehaviour
    {
        public event Action<GameState> GameStateChange = delegate { }; 
        
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private LevelManager _levelManager;
        public LevelManager LevelManager => _levelManager;
        public UIManager UIManager => _uiManager;

        private GameState currentState;
        private int currentLevelId;

        public void Awake()
        {
            currentState = GameState.MAIN_MENU;
            
            _uiManager.Setup(this);
            _levelManager.Setup(this);
            
            ChangeState(currentState);
        }

        public void ChangeState(GameState newState)
        {
            Debug.Log($"Changing state {newState}");
            currentState = newState;
            GameStateChange.Invoke(currentState);
        }

        public void LoadLevel(int levelId)
        {
            currentLevelId = levelId;
            ChangeState(GameState.PLAYING);
            _levelManager.LoadLevel(currentLevelId);
        }
        
    }
}