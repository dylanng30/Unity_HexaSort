using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HexaSort;
using HexaSort.Boosters.Data;
using HexaSort.GameStateMachine.GameStates;
using HexaSort.Level;
using HexaSort.Utilities;
using UnityEngine;

public class MergeController : MonoBehaviour
{
    private LevelManager _levelManager;
    private List<HexaCell> updatedHexaCells = new List<HexaCell>();

    //Combo
    private int _scorePerJelly = 1;
    private int _currentComboCount;

    public void Setup(LevelManager levelManager)
    {
        _levelManager = levelManager;
        updatedHexaCells.Clear();
    }
    
    public void ExecuteMergeSequence(HexaCell startCell, Action onComplete = null)
    {
        StartCoroutine(StackPlacedCoroutine(startCell, onComplete));
    }

    private IEnumerator StackPlacedCoroutine(HexaCell hexaCell, Action onComplete = null)
    {
        _currentComboCount = 0;
        updatedHexaCells.Add(hexaCell);
        
        while (updatedHexaCells.Count > 0)
        {
            yield return CheckMerge(updatedHexaCells[0]);
        }
        
        _levelManager.RemoveMove();
        
        onComplete?.Invoke();
    }
    private IEnumerator CheckMerge(HexaCell hexaCell)
    {
        updatedHexaCells.Remove(hexaCell);

        if (!hexaCell.IsOccupied)
            yield break;
        
        List<HexaCell> neighborHexaCells = HexaAlgorithm.GetNeighborsInRadius(_levelManager._board, hexaCell, 1);
        
        neighborHexaCells.Remove(hexaCell);
        neighborHexaCells.RemoveAll(cell => cell.HexaStack == null);
        
        if (neighborHexaCells.Count <= 0)
        {
            //Debug.Log("No neighbor hexa cells");
            yield break;
        }

        Material hexaCellTopMaterial = hexaCell.HexaStack.GetTopHexaMaterial();

        List<HexaCell> similarNeighborHexaCells = GetSimilarNeighborCells(hexaCellTopMaterial, neighborHexaCells.ToArray());
        
        if (similarNeighborHexaCells.Count <= 0)
        {
            //Debug.Log("No similar neighbor hexa cells");
            yield break;
        }
        
        updatedHexaCells.AddRange(similarNeighborHexaCells);

        List<HexaJelly> hexaJelliesToAdd = GetHexaJelliesToAdd(hexaCellTopMaterial, similarNeighborHexaCells.ToArray());

        RemoveHexaJelliesFromStack(hexaJelliesToAdd, similarNeighborHexaCells.ToArray()); 

        yield return MoveHexaJelliesToStack(hexaCell, hexaJelliesToAdd);

        yield return CheckCompleteStack(hexaCell, hexaCellTopMaterial);
    }   

    
    #region ---HELPER METHODS ---
    
    private List<HexaCell> GetSimilarNeighborCells(Material hexaCellTopMaterial, HexaCell[] neighborHexaCells)
    {
        List<HexaCell> similarNeighborHexaCells = new List<HexaCell>();

        foreach (HexaCell neighborCell in neighborHexaCells)
        {
            if (neighborCell.HexaStack == null) 
                continue;
            
            if (neighborCell.HexaStack.Jellies == null || neighborCell.HexaStack.Jellies.Count == 0)
                continue;
            
            Material neighborCellTopMaterial = neighborCell.HexaStack.GetTopHexaMaterial();

            if (neighborCellTopMaterial.color == hexaCellTopMaterial.color)
                similarNeighborHexaCells.Add(neighborCell);
        }
        
        return similarNeighborHexaCells;
    }

    private List<HexaJelly> GetHexaJelliesToAdd(Material hexaCellTopMaterial, HexaCell[] similarNeighborHexaCells)
    {
        List<HexaJelly> hexaJelliesToAdd = new List<HexaJelly>();

        foreach (HexaCell neighborCell in similarNeighborHexaCells)
        {
            HexaStack neighborStack = neighborCell.HexaStack;

            for (int i = neighborStack.Jellies.Count - 1; i >= 0; i--)
            {
                HexaJelly jelly = neighborStack.Jellies[i];

                if (jelly.Material.color != hexaCellTopMaterial.color)
                    break;
                
                hexaJelliesToAdd.Add(jelly);
                jelly.SetParent(null);
            }
        }
        
        return hexaJelliesToAdd;
    }

    private void RemoveHexaJelliesFromStack(List<HexaJelly> hexaJelliesToAdd, HexaCell[] similarNeighborHexaCells)
    {
        foreach (HexaCell neighborCell in similarNeighborHexaCells)
        {
            HexaStack neighborStack = neighborCell.HexaStack;

            foreach (HexaJelly jelly in hexaJelliesToAdd)
            {
                if (neighborStack.Contains(jelly))
                {
                    neighborStack.Remove(jelly);
                }
            }
        }
    }

    private IEnumerator MoveHexaJelliesToStack(HexaCell hexaCell, List<HexaJelly> hexaJelliesToAdd)
    {
        float timeGap = 0.5f;
        float initialY = hexaCell.HexaStack.Jellies.Count * Constants.HeightHexaModel;

        for (int i = 0; i < hexaJelliesToAdd.Count; i++)
        {
            HexaJelly jelly = hexaJelliesToAdd[i];
            
            float targetY = initialY + i * Constants.HeightHexaModel;
            Vector3 targetLocalPosition = Vector3.up * targetY;
            
            hexaCell.HexaStack.Add(jelly);
            
            jelly.MoveToStack(targetLocalPosition, timeGap);
            yield return new WaitForSeconds(timeGap);
        }
    }

    private IEnumerator CheckCompleteStack(HexaCell hexaCell, Material topMaterial)
    {
        if (hexaCell.HexaStack.Jellies.Count < Constants.MergeThreshold)
        {
            yield break;
        }
        
        List<HexaJelly> similarHexaJellies = new List<HexaJelly>();

        for (int i = hexaCell.HexaStack.Jellies.Count - 1; i >= 0; i--)
        {
            HexaJelly jelly = hexaCell.HexaStack.Jellies[i];

            if (jelly.Material.color != topMaterial.color)
            {
                break;
            }
            
            similarHexaJellies.Add(jelly);
        }

        if (similarHexaJellies.Count < Constants.MergeThreshold)
        {
            yield break;
        }

        //Combo count
        _currentComboCount++;
        
        if (_currentComboCount == 2)
            GameContext.BoosterInventory[BoosterType.Reverse]++;
        else if (_currentComboCount == 3)
            GameContext.BoosterInventory[BoosterType.NormalRocket]++;
        else if (_currentComboCount >= 4)
            GameContext.BoosterInventory[BoosterType.SuperRocket]++;

        while (similarHexaJellies.Count > 0)
        {
            yield return null;
            similarHexaJellies[0].SetParent(null);
            hexaCell.HexaStack.Remove(similarHexaJellies[0]);
            similarHexaJellies[0].Clear();
            similarHexaJellies.RemoveAt(0);

            //Add scores based on combo
            int scoreToAdd = _scorePerJelly * _currentComboCount;
            _levelManager.AddScore(scoreToAdd);
        }
        
        updatedHexaCells.Add(hexaCell);
    }
    #endregion
}
