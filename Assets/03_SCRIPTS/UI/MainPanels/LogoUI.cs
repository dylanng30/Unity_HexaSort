using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace HexaSort.UI.MainPanels
{
    public class LogoUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private Transform _logo;
        
        private Sequence _currentSequence;
        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Effects.DoPopupFX(_logo, 3, out _currentSequence);
            _currentSequence.OnComplete(() =>
            {
                StartCoroutine(StartGame());
            });
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(1f);
            _uiManager.GameManager.ChangeState(GameState.MAIN_MENU);
        }
    }
}