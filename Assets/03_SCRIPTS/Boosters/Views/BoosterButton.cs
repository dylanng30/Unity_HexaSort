using HexaSort.Boosters.Controllers;
using HexaSort.Boosters.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Boosters._04_Presentation.UIs
{
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        
        private BoosterController _controller;
        private BoosterSO _boosterSo;
        public BoosterType Type => _boosterSo.BoosterType;
    
        public void Setup(BoosterController controller, BoosterSO so)
        {
            _controller = controller;
            _boosterSo = so;
            
            if(_icon && so.BoosterIcon)
                _icon.sprite = so.BoosterIcon;
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnClick);
        }
        
        public void UpdateView(int quantity)
        {
            if (_quantityText)
            {
                string quantityText = quantity > 10 ? quantity.ToString() : $"0{quantity}";
                _quantityText.text = quantityText;
            }
        }
        private void OnClick()
        {
            if(_controller == null || _boosterSo == null)
                return;
            
            _controller.SelectBooster(_boosterSo);
        }
    }
}