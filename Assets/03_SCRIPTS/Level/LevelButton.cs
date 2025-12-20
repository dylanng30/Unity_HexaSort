using HexaSort.UI.MainPanels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private int levelId;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text levelName;

        private LevelUI _levelUI;

        public void Setup(LevelUI levelUI,LevelSO levelData)
        {
            _levelUI = levelUI;
            levelId = levelData.LevelId;
            levelName.text = levelId.ToString();
            button.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            _levelUI.LoadLevel(levelId);
        }
    }
}