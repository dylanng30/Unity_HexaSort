using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class PlayingUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private Button _pauseButton;
        

        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
            _pauseButton.onClick.AddListener(Pause);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Pause()
        {
            _uiManager.GameManager.ChangeState(GameState.PAUSE);
        }

    }
}