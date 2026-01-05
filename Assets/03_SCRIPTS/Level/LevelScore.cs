using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelScore : LevelCondition
    {
        private Image _image;
        private int currentScore;
        private int maxScore;

        public LevelScore(LevelManager levelManager) : base(levelManager)
        {
        }
        
        public override void Setup(int value, Image image)
        {
            base.Setup(value, image);
            maxScore = value;
            currentScore = 0;
            _image = image;
            _image.fillAmount = 0;
            UpdateScore();
        }

        public void OnAddScore(int scoreAmount)
        {
            if(_levelManager._gameManager.CurrentState != GameState.MAIN_PLAY)
                return;
            
            currentScore += scoreAmount;
            UpdateScore();
        }

        public bool IsCompleted()
        {
            return currentScore >= maxScore;
        }

        private void UpdateScore()
        {
            float progress = (float) currentScore / maxScore;
            _image.fillAmount = progress;
        }
    }
}