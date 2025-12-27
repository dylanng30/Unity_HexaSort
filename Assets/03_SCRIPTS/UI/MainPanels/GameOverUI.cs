using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class GameOverUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doubleCoinButton;
        
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
            
            _homeButton.onClick.AddListener(BackHome);
            _playAgainButton.onClick.AddListener(PlayAgain);
            _doubleCoinButton.onClick.AddListener(GetDoubleCoin);
        }

        public void Show()
        {
            Debug.Log("GameOverUI Show");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void BackHome()
        {
            _uiManager.GameManager.ChangeState(GameState.MAIN_MENU);
        }

        private void PlayAgain()
        {
            _uiManager.GameManager.LoadLevel();
        }
        
        private void GetDoubleCoin()
        {
            //Ads
        }
    }
}