using System;
using UnityEngine;
using HexaSort.UI.MainPanels;

namespace HexaSort.UI
{
    public class UIManager : MonoBehaviour
    {
        private IMainPanel[]  UIs;
        public GameManager GameManager {get; private set;}

        public void Setup(GameManager gameManager)
        {
            GameManager = gameManager;
            GameManager.GameStateChange += OnGameStateChange;

            LoadUIs();
        }

        private void OnGameStateChange(GameState newState)
        {
            switch (newState)
            {
                case GameState.MAIN_MENU:
                    Show<MenuUI>();
                    break;
                case GameState.LEVEL_BRIEF:
                    Show<LevelBriefUI>();
                    break;
                case GameState.PLAYING:
                    Show<PlayingUI>();
                    break;
                case GameState.GAME_OVER:
                    Show<GameOverUI>();
                    break;
                case GameState.LEVEL_COMPLETED:
                    Show<CompletedUI>();
                    break;
                case GameState.PAUSE:
                    break;
            }
        }
        
        private void Show<T>() where T : IMainPanel
        {
            foreach (var ui in UIs)
            {
                if (ui is T) ui.Show();
                else ui.Hide();
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
        
    }
}