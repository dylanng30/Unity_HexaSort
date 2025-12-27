using System;
using System.Collections.Generic;
using HexaSort.Level;
using HexaSort.MapGenerators;
using HexaSort.ObjectPool;
using HexaSort.Utilitilies;
using UnityEngine;

namespace HexaSort
{
    public class HexaBoard : MonoBehaviour
    {
        [SerializeField] private HexaCell _hexaCellPrefab;
        public Dictionary<Vector3, HexaCell> Map { get; private set; }
        private IMapGenerator _currentMapGenerator;
        private BaseObjectPool<HexaCell> _pool;

        private void Awake()
        {
            _pool = new BaseObjectPool<HexaCell>(_hexaCellPrefab, transform);
        }

        public void Setup(IMapGenerator mapGenerator, LevelSO levelData)
        {
            _currentMapGenerator = mapGenerator;
            CreateGrid(levelData.GridWidth, levelData.GridHeight);
        }
        
        private void CreateGrid(int width, int height)
        {
            Map = _currentMapGenerator.CreateMap(_pool, width, height);
        }
        
        public void Clear()
        {
            if (Map == null)
                return;
        
            foreach (var key in Map.Keys)
            {
                Map[key].Clear();
                _pool.Return(Map[key]);
            }
        
            Map.Clear();
        }
    }
}