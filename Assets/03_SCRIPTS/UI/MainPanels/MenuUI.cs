using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class MenuUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        private Coroutine _popButtonCoroutine;

        private void Awake()
        {
            _playButton?.onClick.AddListener(Play);
        }

        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            //Debug.Log("MenuUI Show");
            gameObject.SetActive(true);

            string _levelId = GameContext.CurrentLevel.ToString();
            _levelText.text = "Level " + _levelId;

            Effects.DoHeartbeatFX(_playButton.transform);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Play()
        {
            _uiManager?.LoadLevel();
        }
    }
}