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

        public LevelMoves(LevelManager levelManager) : base(levelManager)
        {
        }
        
        public override void Setup(int value, TextMeshProUGUI text)
        {
            base.Setup(value);
            remainMoves = value;
            _text = text;
            UpdateText();
        }

        public void OnMove()
        {
            if(_levelManager._gameManager.CurrentState != GameState.PLAYING)
                return;
            
            remainMoves--;
            
            UpdateText();
        }
        public bool IsCompleted()
        {
            return remainMoves <= 0;
        }
        

        public void UpdateText()
        {
            _text.text = remainMoves.ToString();
        }
    }
}