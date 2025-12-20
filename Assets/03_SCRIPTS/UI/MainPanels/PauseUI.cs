using UnityEngine;

namespace HexaSort.UI.MainPanels
{
    public class PauseUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            Debug.Log("PauseUI Show");
        }

        public void Hide()
        {
        }
    }
}