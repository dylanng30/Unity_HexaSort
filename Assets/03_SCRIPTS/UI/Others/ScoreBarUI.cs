using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.UI.Others
{
    public class ScoreBarUI : MonoBehaviour
    {
        [SerializeField] private Image scoreBar;

        public void UpdateScoreBar(float scorePercentage)
        {
            scoreBar.fillAmount = scorePercentage;
        }
    }
}