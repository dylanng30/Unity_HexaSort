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

        public GameState CurrentState { get; private set; }
        private int currentLevelId;

        public void Awake()
        {
            CurrentState = GameState.MAIN_MENU;
            
            _uiManager.Setup(this);
            _levelManager.Setup(this);
            
            ChangeState(CurrentState);
        }

        public void ChangeState(GameState newState)
        {
            Debug.Log($"Changing state {newState}");
            CurrentState = newState;
            GameStateChange.Invoke(CurrentState);
        }

        public void LoadLevel(int levelId)
        {
            currentLevelId = levelId;
            ChangeState(GameState.PLAYING);
            _levelManager.LoadLevel(currentLevelId);
        }
        
    }
}