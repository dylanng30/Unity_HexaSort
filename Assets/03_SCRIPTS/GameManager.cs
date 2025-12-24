using System;
using HexaSort.Level;
using HexaSort.UI;
using UnityEngine;

namespace HexaSort
{
    public enum GameState
    {
        MAIN_MENU,
        LEVEL_BRIEF,
        PLAYING,
        PAUSE,
        GAME_OVER,
        LEVEL_COMPLETED
    }
    public class GameManager : MonoBehaviour
    {
        public event Action<GameState> GameStateChange = delegate { }; 
        
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private LevelManager _levelManager;
        public LevelManager LevelManager => _levelManager;
        public UIManager UIManager => _uiManager;

        public GameState CurrentState { get; private set; }
        public int CurrentLevelId {get; private set;}

        public void Awake()
        {
            CurrentState = GameState.MAIN_MENU;
            CurrentLevelId = 1;
            
            _uiManager.Setup(this);
            _levelManager.Setup(this);
            
            ChangeState(CurrentState);
        }

        public void ChangeState(GameState newState)
        {
            Debug.Log($"[GAME MANAGER] Changing state {newState}");
            CurrentState = newState;
            GameStateChange.Invoke(CurrentState);
        }

        public void CompleteLevel()
        {
            CurrentLevelId++;
        }
        
        public void LoadLevel()
        {
            _levelManager.LoadLevel(CurrentLevelId);
            ChangeState(GameState.LEVEL_BRIEF);
        }
    }
}