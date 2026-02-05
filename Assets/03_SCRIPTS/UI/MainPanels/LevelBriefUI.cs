using System.Collections;
using DG.Tweening;
using HexaSort;
using HexaSort.Level;
using HexaSort.UI;
using HexaSort.UI.MainPanels;
using TMPro;
using UnityEngine;

public class LevelBriefUI : MonoBehaviour, IMainPanel
{
    private UIManager _uiManager;

    [Header("UI Elements")] 
    [SerializeField] private Transform _modal;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _targetScoreText;

    private Sequence _currentSequence;
    public void Setup(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        
        UpdateLevelInfo();
        
        Effects.DoPopupFX(_modal, 1, out _currentSequence);
        _currentSequence.OnComplete(() =>
        {
            StartCoroutine(ChangeStateCoroutine());
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateLevelInfo()
    {
        LevelSO currentLevel = _uiManager.GameManager.LevelManager.GetCurrentLevelData();

        if (currentLevel != null)
        {
            if (_titleText)
            {
                var title = $"--- Level {currentLevel.LevelId.ToString()} ---"; 
                _titleText.text = title;
            }
                
            if (_targetScoreText) 
                _targetScoreText.text = currentLevel.TargetGoal.ToString();
        }
    }

    private IEnumerator ChangeStateCoroutine()
    {
        yield return new WaitForSeconds(3f);
        _uiManager.GameManager.ChangeState(GameState.MAIN_PLAY);
    }
}
