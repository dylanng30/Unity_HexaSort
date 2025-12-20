using System;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class MenuUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        [SerializeField] private Button _playButton;

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
            Debug.Log("MenuUI Show");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Play()
        {
            _uiManager?.LoadLevelPanel();
        }
        
    }
}