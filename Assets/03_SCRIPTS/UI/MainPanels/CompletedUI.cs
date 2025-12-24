using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class CompletedUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doubleCoinButton;
        
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
            _nextLevelButton.onClick.AddListener(NextLevel);
            _homeButton.onClick.AddListener(BackHome);
            _doubleCoinButton.onClick.AddListener(GetDoubleCoin);
        }

        public void Show()
        {
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

        private void NextLevel()
        {
            _uiManager.LoadLevel();
        }
        
        private void GetDoubleCoin()
        {
            //Ads
        }
    }
}