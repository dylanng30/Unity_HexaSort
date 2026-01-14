using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexaSort.Boosters._04_Presentation.UIs;
using HexaSort.Boosters.BoosterLogics;
using HexaSort.Boosters.Data;
using HexaSort.Utilitilies;
using TMPro;
using UnityEngine;

namespace HexaSort.Boosters._04_Presentation.Controllers
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private BoosterButton _boosterButtonPrefab;
        [SerializeField] private Transform _boosterContainer;
        [SerializeField] private HexaBoard _board;

        [SerializeField] private Transform _rocketSpawner;
        [SerializeField] private GameObject _rocketPrefab;
        
        private Dictionary<BoosterType, BoosterSO> _boosterDataDic;
        private List<BoosterButton> _boosterButtons;
        private GameManager _gameManager;
        
        private BoosterSO currentSelectedBooster;
        private IBoostLogic currentLogic;
        private bool isTargeting = false;

        public void Setup(GameManager gameManager)
        {
            _gameManager = gameManager;
            _boosterButtons = new List<BoosterButton>();
            LoadBoosterDatas();
            StartCoroutine(CreateBoosterButtons());
        }
        
        public void SelectBooster(BoosterSO so)
        {
            //Check Inventory
            if(GameContext.BoosterInventory[so.BoosterType] <= 0)
                return;
            
            Debug.Log($"Selected booster {so.BoosterType}");
            
            _nameText.text = so.BoosterName;
            _infoText.text = so.BoosterDescription;
            
            _gameManager.ChangeState(GameState.USE_BOOSTER);
            
            currentSelectedBooster = so;
            currentLogic = GetCurrentBoosterLogic(so.BoosterType);
            
            isTargeting = true;
        }

        public void OnHexaCellClicked(HexaCell cell)
        {
            if (!isTargeting)
                return;
            
            var rocket = Instantiate(_rocketPrefab, _rocketSpawner.position,  Quaternion.identity);
            Effects.DoArcMove(rocket.transform, cell.transform.position, 2f, 5f, () =>
            {
                Destroy(rocket.gameObject);
                currentLogic.Execute(_board, cell, OnBoosterFinished);
            });
        }
        
        private void OnBoosterFinished()
        {
            Debug.Log(currentSelectedBooster.BoosterType);
            
            //Test
            //GameContext.BoosterInventory[currentSelectedBooster.BoosterType]--;
            
            isTargeting = false;
            currentLogic = null;
            currentSelectedBooster = null;
            
            UpdateButtonViews();
            
            _gameManager.ChangeState(GameState.MAIN_PLAY);
        }
        
        private void LoadBoosterDatas()
        {
            var boosterDatas = Resources.LoadAll<BoosterSO>(Constants.BoosterDataPath);
            _boosterDataDic = boosterDatas.ToDictionary(b => b.BoosterType, b => b);
        }

        private IEnumerator CreateBoosterButtons()
        {
            yield return new WaitUntil(() => _boosterDataDic != null);
            
            foreach (var boosterType in _boosterDataDic.Keys)
            {
                var button = Instantiate(_boosterButtonPrefab, _boosterContainer);
                _boosterButtons.Add(button);
                var boosterData = _boosterDataDic[boosterType];
                button.Setup(this, boosterData);
            }

            UpdateButtonViews();
        }

        private void UpdateButtonViews()
        {
            foreach (var button in _boosterButtons)
            {
                if (!GameContext.BoosterInventory.ContainsKey(button.Type)) continue;

                var quantity = GameContext.BoosterInventory[button.Type];
                button.UpdateView(quantity);
            }
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