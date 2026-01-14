using DG.Tweening;
using UnityEngine;

namespace HexaSort.UI.MainPanels
{
    public class UsingBoosterUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private RectTransform BoosterInfo;

        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Effects.DoNotificationFX(BoosterInfo, 1f, out Sequence sequence);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
    }
}