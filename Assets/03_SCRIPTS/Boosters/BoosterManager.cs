using System;
using System.Collections.Generic;
using HexaSort.Boosters.BoosterLogics;
using HexaSort.Boosters.Data;
using HexaSort.Utilitilies;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Boosters
{
    [Serializable]
    public struct BoosterButton
    {
        public Button _button;
        public BoosterData _data;
    }
    public class BoosterManager : Singleton<BoosterManager>
    {
        [SerializeField] private HexaBoard _board;
        private BoosterData currentSelectedBooster;
        private IBoostLogic currentLogic;
        private bool isTargeting = false;

        [SerializeField] private List<BoosterButton> _boosterButtons = new List<BoosterButton>();

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _boosterButtons.Count; i++)
            {
                var data = _boosterButtons[i]._data;
                _boosterButtons[i]._button.onClick.AddListener(() => SelectBooster(data));
            }
        }

        public void SelectBooster(BoosterData data)
        {
            //Check Inventory
            Debug.Log($"Selected booster {data.BoosterType}");
            
            currentSelectedBooster = data;
            currentLogic = GetCurrentBoosterLogic(data.BoosterType);
            
            isTargeting = true;
            //Do Logic
        }

        public void OnHexaCellClicked(HexaCell cell)
        {
            if(isTargeting)
                currentLogic.Execute(_board, cell, OnBoosterFinished);
        }

        private void OnBoosterFinished()
        {
            //Remove in Inventory
            
            isTargeting = false;
            currentLogic = null;
            currentSelectedBooster = null;
        }
        

        private IBoostLogic GetCurrentBoosterLogic(BoosterType type)
        {
            
            switch (type)
            {
                case BoosterType.NormalRocket:
                    return new NormalRocketLogic();
                case BoosterType.SuperRocket:
                    return new SuperRocketLogic();
                case BoosterType.Swap:
                    return new SwapLogic();
                default:
                    return new EmptyBoosterLogic();
            }
        }
    }
}