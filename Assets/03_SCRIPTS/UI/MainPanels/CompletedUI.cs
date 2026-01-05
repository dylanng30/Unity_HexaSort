using DG.Tweening;
using HexaSort.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class CompletedUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;

        [SerializeField] private Transform _modal;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _doubleCoinButton;
        [SerializeField] private TextMeshProUGUI _levelTitle;
        
        private Sequence _currentSequence;
        
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
            UpdateLevelText();
            Effects.DoPopupFX(_modal, 1, out _currentSequence);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void BackHome()
        {
            if (_currentSequence != null && _currentSequence.IsActive())
                _currentSequence.Kill();
            
            Effects.DoPopdownFX(_modal, 1, out _currentSequence);
            
            _currentSequence.OnComplete(() =>
            {
                _uiManager.GameManager.ChangeState(GameState.MAIN_MENU);
            });
        }

        private void NextLevel()
        {
            if (_currentSequence != null && _currentSequence.IsActive()) 
                _currentSequence.Kill();
            
            Effects.DoPopdownFX(_modal, 1, out _currentSequence);
            
            _currentSequence.OnComplete(() =>
            {
                _uiManager.LoadLevel();
            });
        }
        
        private void GetDoubleCoin()
        {
            //Ads
        }

        private void UpdateLevelText()
        {
            LevelSO currentLevel = _uiManager.GameManager.LevelManager.GetCurrentLevelData();
            var levelTitle = $"Level {currentLevel.LevelId.ToString()}";
            _levelTitle.text = levelTitle;
        }
    }
}