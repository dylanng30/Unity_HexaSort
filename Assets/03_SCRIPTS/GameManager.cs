using System;
using System.Collections.Generic;
using HexaSort.Boosters;
using HexaSort.Boosters.Controllers;
using HexaSort.Boosters.Data;
using HexaSort.GameStateMachine;
using HexaSort.GameStateMachine.GameStates;
using HexaSort.GameStateMachine.Interfaces;
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
        MAIN_PLAY,
        MERGE,
        USE_BOOSTER,
        PAUSE,
        LEVEL_FAILED,
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
        private string saveFileName = "player_save.json";
        
        [field: SerializeField] public MergeController MergeController { get; private set; }
        [field: SerializeField] public BoosterController BoosterController { get; private set; }
        
        private StateMachine _stateMachine;
        private Dictionary<GameState, IGameState> _gameStates;
        
        public HexaCell LastPlacedHexaCell;

        public void Awake()
        {
            _stateMachine = new StateMachine();
            
            LoadGame();
            
            LoadGameStates();
        }

        private void Start()
        {
            CurrentState = GameState.LOGO;
            
            _uiManager.Setup(this);
            _levelManager.Setup(this);
            BoosterController.Setup(this);
            
            MergeController.Setup(_levelManager);
            
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

        private void Update()
        {
            if(_stateMachine == null) 
                return;
            
            _stateMachine.HandleInput();
            _stateMachine.Update();
        }

        public void ChangeState(GameState newState)
        {
            CurrentState = newState;
            //Debug.Log($"[GAME MANAGER] Changing state {newState}");
            GameStateChange.Invoke(CurrentState);
            
            if (_gameStates.ContainsKey(newState))
            {
                _stateMachine.ChangeState(_gameStates[CurrentState]);
            }
        }

        public void CompleteLevel()
        {
            GameContext.CurrentLevel++;
        }
        
        public void LoadLevel()
        {
            _levelManager.LoadLevel(GameContext.CurrentLevel);
            ChangeState(GameState.LEVEL_BRIEF);
        }
        
        public void LoadGame()
        {
            PlayerData loadedData = SaveSystem.Load<PlayerData>(saveFileName);
            loadedData = loadedData != null ? loadedData : new PlayerData();
            
            GameContext.CurrentLevel = loadedData.Level;
            GameContext.BoosterInventory[BoosterType.NormalRocket] = loadedData.NormalRocket;
            GameContext.BoosterInventory[BoosterType.SuperRocket] = loadedData.SuperRocket;
            GameContext.BoosterInventory[BoosterType.Reverse] = loadedData.Swap;
        }
        public void SaveGame()
        {
            PlayerData saveData = new PlayerData()
            {
                Level = GameContext.CurrentLevel,
                NormalRocket = GameContext.BoosterInventory[BoosterType.NormalRocket],
                SuperRocket = GameContext.BoosterInventory[BoosterType.SuperRocket],
                Swap = GameContext.BoosterInventory[BoosterType.Reverse]
            };
            SaveSystem.Save<PlayerData>(saveData, saveFileName);
        }

        private void LoadGameStates()
        {
            _gameStates = new Dictionary<GameState, IGameState>()
            {
                { GameState.LOGO , new LogoGameState(this)},
                { GameState.MAIN_MENU , new MainMenuGameState(this)},
                { GameState.LEVEL_BRIEF, new LevelBriefGameState(this)},
                { GameState.MAIN_PLAY , new MainPlayGameState(this)},
                { GameState.USE_BOOSTER , new UseBoosterGameState(this)},
                { GameState.MERGE , new MergeGameState(this)},
                { GameState.PAUSE , new PauseGameState(this)},
                { GameState.LEVEL_COMPLETED, new LevelCompletedGameState(this)},
                { GameState.LEVEL_FAILED, new LevelFailedGameState(this)}
            };
        }
    }
}