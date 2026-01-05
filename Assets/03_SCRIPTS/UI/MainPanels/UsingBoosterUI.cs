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
            Effects.DoNotificationFX(BoosterInfo, 2f, out Sequence sequence);
        }

        public void Hide()
        {
            /*Effects.DoInverseNotificationFX(BoosterInfo, 2f, out Sequence sequence);
            
            sequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
                _uiManager.GameManager.ChangeState(GameState.MAIN_PLAY);
            });*/
        }
    }
}