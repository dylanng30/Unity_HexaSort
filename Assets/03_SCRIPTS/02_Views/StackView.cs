using HexaSort._01_Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort._02_Views
{
    public class StackView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _similarJellyNumberOnTopText;
        public StackModel Model { get; private set; }
        public void Setup(StackModel model)
        {
            Model = model;
            Model.OnJelliesAdded += DisplaySimilarJellyNumberOnTop;
        }

        public void Clear()
        {
            Model.OnJelliesAdded -= DisplaySimilarJellyNumberOnTop;
            Model = null;
        }

        private void DisplaySimilarJellyNumberOnTop()
        {
            if (_similarJellyNumberOnTopText != null && Model != null)
                _similarJellyNumberOnTopText.text = Model.GetTopJellyFruit.ToString();
        }
        
    }
}