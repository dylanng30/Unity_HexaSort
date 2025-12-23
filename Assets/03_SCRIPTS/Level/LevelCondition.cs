using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public enum ConditionType
    {
        NoEnd, GoodEnd, BadEnd
    }
    public class LevelCondition
    {
        protected LevelManager _levelManager;

        public LevelCondition(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public virtual void Setup(float value) {}
        public virtual void Setup(int value, TextMeshProUGUI text) {}
        public virtual void Setup(int value, Image image) {}

        protected void OnConditionComplete(ConditionType _conditionType)
        {
            //Call UI(popup)
            _levelManager.CompleteLevel(_conditionType);
        }
    }
}