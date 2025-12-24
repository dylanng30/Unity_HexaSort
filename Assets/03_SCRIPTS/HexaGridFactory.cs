using System;
using System.Collections;
using System.Collections.Generic;
using HexaSort.ObjectPool;
using NaughtyAttributes;
using UnityEngine;

public class HexaGridFactory : MonoBehaviour
{
    [Header("---REFERENCES---")]
    [SerializeField] private Grid _grid;
    [SerializeField] private HexaCell _hexaPrefab;
    
    private int _gridSize;
    
    private BaseObjectPool<HexaCell> _hexaCellPool;
    public List<HexaCell> HexagonCells { get; private set; }
    

    public void Setup(int gridSize)
    {
        _hexaCellPool = _hexaCellPool != null ? _hexaCellPool : new BaseObjectPool<HexaCell>(_hexaPrefab, this.transform);
        HexagonCells = HexagonCells != null ? HexagonCells : new List<HexaCell>();
        
        _gridSize = gridSize;
        CreateGrid();
    }
    
    private void CreateGrid()
    {
        for (int i = -_gridSize; i <= _gridSize; i++)
        {
            for (int j = -_gridSize; j <= _gridSize; j++)
            {
                Vector3 spawnPosition = _grid.CellToWorld(new Vector3Int(i, j, 0));

                if (spawnPosition.magnitude > _grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * _gridSize) 
                    continue;
                
                var hexaCell = _hexaCellPool.Get();
                hexaCell.transform.position = spawnPosition;
                HexagonCells.Add(hexaCell);
            }
        }
    }

    public void Clear()
    {
        if (HexagonCells == null || HexagonCells.Count == 0) 
            return;
        
        foreach (var hexagon in HexagonCells)
        {
            hexagon.Clear();
            _hexaCellPool.Return(hexagon);
        }
        
        HexagonCells.Clear();
    }
    
}
