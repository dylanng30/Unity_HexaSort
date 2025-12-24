using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _targetScoreText;
    [SerializeField] private TextMeshProUGUI _moveLimitText;
    [SerializeField] private TextMeshProUGUI _timeLimitText;

    public void Setup(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(StartBriefCoroutine());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator StartBriefCoroutine()
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

            if (_moveLimitText) 
                _moveLimitText.text = currentLevel.MoveLimit.ToString();

            if (_timeLimitText)
            {
                if (currentLevel.TimeLimit > 0)
                    _timeLimitText.text = currentLevel.TimeLimit.ToString() + "s";
                else
                    _timeLimitText.text = "No Limit";
            }
        }

        yield return new WaitForSeconds(3f);

        _uiManager.GameManager.ChangeState(GameState.PLAYING);
    }
}
