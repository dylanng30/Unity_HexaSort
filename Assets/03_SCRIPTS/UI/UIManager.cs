using System;
using System.Collections.Generic;
using HexaSort.GameStateMachine.GameStates;
using UnityEngine;
using HexaSort.UI.MainPanels;

namespace HexaSort.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameManager GameManager {get; private set;}
        private IMainPanel[]  UIs;
        private Dictionary<GameState, Type> _stateToUiMap;

        public void Setup(GameManager gameManager)
        {
            GameManager = gameManager;
            GameManager.GameStateChange += OnGameStateChange;
            LoadUIs();
            InitStateMapping();
        }

        private void OnGameStateChange(GameState newState)
        {
            if (_stateToUiMap.TryGetValue(newState, out Type uiType))
            {
                ShowUIByType(uiType);
            }
        }
        private void ShowUIByType(Type uiType)
        {
            foreach (var ui in UIs)
            {
                if (ui.GetType() == uiType) 
                    ui.Show();
                else 
                    ui.Hide();
            }
        }

        public void LoadLevel()
        {
            GameManager.LoadLevel();
        }

        private void LoadUIs()
        {
            UIs = GetComponentsInChildren<IMainPanel>(true);
            
            foreach (var ui in UIs)
            {
                ui.Setup(this);
            }
        }
        
        private void InitStateMapping()
        {
            _stateToUiMap = new Dictionary<GameState, Type>
            {
                { GameState.LOGO, typeof(LogoUI) },
                { GameState.MAIN_MENU, typeof(MenuUI) },
                { GameState.LEVEL_BRIEF, typeof(LevelBriefUI) },
                { GameState.MAIN_PLAY, typeof(PlayingUI) },
                { GameState.USE_BOOSTER, typeof(UsingBoosterUI) },
                { GameState.LEVEL_FAILED, typeof(GameOverUI) },
                { GameState.LEVEL_COMPLETED, typeof(CompletedUI) },
                { GameState.PAUSE, typeof(PauseUI) }
            };
        }
        
    }
}