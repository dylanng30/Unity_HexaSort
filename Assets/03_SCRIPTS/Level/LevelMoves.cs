using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelMoves : LevelCondition
    {
        private int remainMoves; 
        private TextMeshProUGUI _text;

        public LevelMoves(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        
        public override void Setup(int value, TextMeshProUGUI text)
        {
            base.Setup(value);
            currentConditionType = ConditionType.NoEnd;
            remainMoves = value;
            _text = text;
            UpdateText();
        }

        public void OnMove()
        {
            if(currentConditionType != ConditionType.NoEnd)
                return;
            
            remainMoves--;
            
            UpdateText();

            if (remainMoves <= 0)
            {
                currentConditionType = ConditionType.BadEnd;
                OnConditionComplete();
            }
        }

        public void UpdateText()
        {
            _text.text = remainMoves.ToString();
        }
    }
}