using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexaSort.ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelManager : MonoBehaviour
    {
        public GameManager _gameManager;
        
        [Header("---REFERENCES---")]
        [SerializeField] private HexaGridFactory _gridFactory;
        [SerializeField] private StackSpawner _stackSpawner;
        [SerializeField] private StackController _stackController;
        [SerializeField] private MergeManager _mergeManager;
        
        [Header("---UI---")]
        [SerializeField] private TextMeshProUGUI _moveText;
        [SerializeField] private Image _scoreImage;
        
        //Level
        public Dictionary<int, LevelSO> LevelDictionary { get; private set; }
        private LevelSO _currentLevelData;
        
        //Conditions
        private LevelMoves _moveCondition;
        private LevelScore _scoreCondition;
        
        private void Awake()
        {
            _moveCondition = new LevelMoves(this);
            _scoreCondition = new LevelScore(this);
        }

        public void Setup(GameManager gameManager)
        {
            _gameManager = gameManager;
            LoadAllLevels();
        }

        private void LoadAllLevels()
        {
            LevelSO[] levels = Resources.LoadAll<LevelSO>("Levels");
            LevelDictionary = levels.ToDictionary(l => l.LevelId, l => l);
        }

        public void LoadLevel(int levelId)
        {
            ClearCurrentLevel();
            
            if (LevelDictionary.TryGetValue(levelId, out LevelSO levelData))
            {
                Debug.Log($"[LEVEL MANAGER] Loading Level {levelId}");
                SetupLevel(levelData);
                return;
            }
            
            Debug.LogError($"Level {levelId} not found!");
        }

        private void SetupLevel(LevelSO levelData)
        {
            if (_moveText == null || _scoreImage == null)
            {
                Debug.LogError("Let's inject UI components");
                return;
            }
            
            _currentLevelData = levelData;
            
            //Conditions
            _scoreCondition.Setup(_currentLevelData.TargetGoal, _scoreImage);
            _moveCondition.Setup(levelData.MoveLimit, _moveText);
            
            //Map
            _stackSpawner.Setup(levelData.MiniumHexaAmount, levelData.MaxiumHexaAmount, levelData.Materials);
            _gridFactory.Setup(levelData.GridSize);
            
            //Logic gameplay
            _mergeManager.Setup(this, levelData.MergeCount);
        }
        

        public void AddScore()
        {
            _scoreCondition.OnAddScore();
        }

        public void RemoveMove()
        {
            _moveCondition.OnMove();
        }

        public void CheckComplete()
        {
            if(_scoreCondition.IsCompleted())
            {
                _gameManager.CompleteLevel();
                _gameManager.ChangeState(GameState.LEVEL_COMPLETED);
            }
            else if (_moveCondition.IsCompleted())
            {
                _gameManager.ChangeState(GameState.GAME_OVER);
            }
        }
        
        private void ClearCurrentLevel()
        {
            if (_gridFactory != null) 
                _gridFactory.Clear();
            
            if (_stackSpawner != null) 
                _stackSpawner.Clear();
        }
    }
}