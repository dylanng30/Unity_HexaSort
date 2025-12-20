using System.Collections.Generic;
using System.Linq;
using HexaSort.Level;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.MainPanels
{
    public class LevelUI : MonoBehaviour, IMainPanel
    {
        private UIManager _uiManager;
        
        [SerializeField] private LevelButton levelButtonPrefab;
        [SerializeField] private Transform buttonsContainer;
        
        [SerializeField] private Color[] colors;

        public void Setup(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void Show()
        {
            Debug.Log("LevelUI Show");
            gameObject.SetActive(true);
            CreateLevelButtons();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void CreateLevelButtons()
        {
            var levelDic = _uiManager.GameManager.LevelManager.LevelDictionary;
            
            for (int i = 1; i <= levelDic.Count; i++)
            {
                var button = Instantiate(levelButtonPrefab, buttonsContainer);
                button.Setup(this, levelDic[i]);
                
                //SetupColor - Temp
                var image = button.GetComponent<Image>();
                image.color = GetColor(i);
            }
        }

        public void LoadLevel(int levelId)
        {
            _uiManager.LoadLevel(levelId);
        }

        private Color GetColor(int index)
        {
            return colors[(index - 1) % colors.Length];
        }
        
    }
}