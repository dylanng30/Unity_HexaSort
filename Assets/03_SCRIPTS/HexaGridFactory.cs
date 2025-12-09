using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class HexaGridFactory : MonoBehaviour
{
    [Header("---REFERENCES---")]
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _hexaPrefab;

    [Header("---SETTINGS---")]
    [OnValueChanged("CreateGrid")]
    [SerializeField] private int _gridSize;
    
    private void CreateGrid()
    {
        transform.Clear();
        
        for (int i = -_gridSize; i <= _gridSize; i++)
        {
            for (int j = -_gridSize; j <= _gridSize; j++)
            {
                Vector3 spawnPosition = _grid.CellToWorld(new Vector3Int(i, j, 0));

                if (spawnPosition.magnitude > _grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * _gridSize) continue;
                
                Instantiate(_hexaPrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
    
}
