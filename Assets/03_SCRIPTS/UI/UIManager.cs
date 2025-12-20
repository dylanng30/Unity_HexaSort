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
            Debug.Log($"GameState changed to {newState}");
            switch (newState)
            {
                case GameState.MAIN_MENU:
                    Show<MenuUI>();
                    break;
                case GameState.LEVEL_MENU:
                    Show<LevelUI>();
                    break;
                case GameState.PLAYING:
                    Show<PlayingUI>();
                    break;
                case GameState.PAUSE:
                    break;
                case GameState.GAME_OVER:
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

        public void LoadLevelPanel()
        {
            GameManager.ChangeState(GameState.LEVEL_MENU);
        }

        public void LoadLevel(int levelId)
        {
            GameManager.LoadLevel(levelId);
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