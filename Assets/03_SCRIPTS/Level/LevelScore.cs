using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelScore : LevelCondition
    {
        private Image _image;
        private int currentScore;
        private int maxScore;

        public LevelScore(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        
        public override void Setup(int value, Image image)
        {
            base.Setup(value, image);
            maxScore = value;
            currentScore = 0;
            _image = image;
            _image.fillAmount = 0;
            currentConditionType = ConditionType.NoEnd;
            UpdateScore();
        }

        public void OnAddScore()
        {
            if(currentConditionType != ConditionType.NoEnd)
                return;
            
            currentScore++;
            Debug.Log($"Current score {currentScore}");
            UpdateScore();

            if (currentScore >= maxScore)
            {
                currentConditionType = ConditionType.GoodEnd;
            }
        }

        public void UpdateScore()
        {
            float progress = (float) (currentScore / maxScore);
            _image.fillAmount = progress;
        }
    }
}