using UnityEngine;

namespace HexaSort.UI.MainPanels
{
    public class PlayingUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;

        //[SerializeField] private
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            //Debug.Log("PlayingUI Show");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}