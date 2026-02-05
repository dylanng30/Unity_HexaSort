using System.Collections.Generic;
using System.Linq;
using HexaSort.MapGenerators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexaSort.Level
{
    public class LevelManager : MonoBehaviour
    {
        public GameManager _gameManager;
        
        [Header("---REFERENCES---")]
        private IMapGenerator _currentMapGenerator;
        public HexaBoard _board;
        [SerializeField] private StackSpawner _stackSpawner;
        
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
            _currentMapGenerator = new HexaMapGenerator();
            
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

        public LevelSO GetCurrentLevelData()
        {
            return _currentLevelData;
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
            _board?.Setup(_currentMapGenerator, _currentLevelData);
        }
        

        public void AddScore(int scoreAmount)
        {
            _scoreCondition.OnAddScore(scoreAmount);
        }

        public void RemoveMove()
        {
            _moveCondition.OnMove();
        }
        
        public void OnPlayerMoveFinished()
        {
            _stackSpawner.NotifyStackPlaced();
        }

        public void CheckSpawnStacks()
        {
            _stackSpawner.CheckAndSpawnNewStacks();
        }

        public GameState GetLevelStatus()
        {
            if (_scoreCondition.IsCompleted())
            {
                _gameManager.CompleteLevel();
                return GameState.LEVEL_COMPLETED;
            }
            if (_moveCondition.IsCompleted())
            {
                return GameState.LEVEL_FAILED;
            }

            return GameState.MAIN_PLAY;
        }
        
        private void ClearCurrentLevel()
        {
            if (_stackSpawner != null) 
                _stackSpawner.Clear();
        }
    }
}