using UnityEngine;

namespace HexaSort.UI.MainPanels
{
    public class GameOverUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            Debug.Log("GameOverUI Show");
        }

        public void Hide()
        {
        }
    }
}