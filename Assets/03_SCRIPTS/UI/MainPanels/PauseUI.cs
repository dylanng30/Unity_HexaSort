using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class PauseUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _playAgainButton;
        
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
            
            _resumeButton.onClick.AddListener(Resume);
            _homeButton.onClick.AddListener(BackHome);
            _playAgainButton.onClick.AddListener(PlayAgain);
        }

        public void Show()
        {
            Debug.Log("PauseUI Show");
            gameObject.SetActive(true);
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Resume()
        {
            _uiManager.GameManager.ChangeState(GameState.MAIN_PLAY);
        }

        private void BackHome()
        {
            _uiManager.GameManager.ChangeState(GameState.MAIN_MENU);
        }

        private void PlayAgain()
        {
            _uiManager.GameManager.LoadLevel();
        }
    }
}