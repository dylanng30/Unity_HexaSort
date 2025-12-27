using System;
using HexaSort.Level;
using HexaSort.SaveLoad;
using HexaSort.UI;
using UnityEngine;
using HexaSort.SaveLoadSystem;

namespace HexaSort
{
    public enum GameState
    {
        LOGO,
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
        
        public PlayerData currentData;
        private string saveFileName = "player_save.json";
        

        public void Awake()
        {
            LoadGame();
        }

        private void Start()
        {
            CurrentState = GameState.LOGO;
            CurrentLevelId = currentData.currentUnlockedLevel;
            
            _uiManager.Setup(this);
            _levelManager.Setup(this);
            
            ChangeState(CurrentState);
        }

        private void OnApplicationQuit()
        {
            SaveGame(); 
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveGame();
            }
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
            currentData.currentUnlockedLevel = CurrentLevelId;
        }
        
        public void LoadLevel()
        {
            _levelManager.LoadLevel(CurrentLevelId);
            ChangeState(GameState.LEVEL_BRIEF);
        }
        
        public void LoadGame()
        {
            PlayerData loadedData = SaveSystem.Load<PlayerData>(saveFileName);

            if (loadedData != null)
            {
                currentData = loadedData;
            }
            else
            {
                currentData = new PlayerData();
                Debug.Log("Tạo dữ liệu mới.");
            }
        }
        public void SaveGame()
        {
            SaveSystem.Save<PlayerData>(currentData, saveFileName);
        }
    }
}