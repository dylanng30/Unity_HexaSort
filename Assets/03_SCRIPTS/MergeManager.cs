using System;
using System.Collections;
using System.Collections.Generic;
using HexaSort.Level;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private List<HexaCell> updatedHexaCells = new List<HexaCell>();
    private int _mergeCount;
    private Action _onScoreAdded;
    private Action _onMove;

    public static bool FinishMerge;
    private void OnEnable()
    {
        StackController.OnStackPlaced += StackPlacedCallBack;
    }

    private void OnDisable()
    {
        StackController.OnStackPlaced -= StackPlacedCallBack;
    }

    public void Setup(int mergeCount, Action onScoreAdded,  Action onMove)
    {
        _mergeCount = mergeCount;
        _onScoreAdded = onScoreAdded;
        _onMove = onMove;
        FinishMerge = true;
    }

    private void StackPlacedCallBack(HexaCell hexaCell)
    {
        StartCoroutine(StackPlacedCoroutine(hexaCell));
    }

    private IEnumerator StackPlacedCoroutine(HexaCell hexaCell)
    {
        FinishMerge = false;
        updatedHexaCells.Add(hexaCell);
        
        while (updatedHexaCells.Count > 0)
        {
            yield return CheckMerge(updatedHexaCells[0]);
        }
        
        _onMove.Invoke();
        FinishMerge = true;
    }
    private IEnumerator CheckMerge(HexaCell hexaCell)
    {
        updatedHexaCells.Remove(hexaCell);

        if (!hexaCell.IsOccupied)
            yield break;
        
        List<HexaCell> neighborHexaCells = GetNeighborCells(hexaCell);
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
    private List<HexaCell> GetNeighborCells(HexaCell hexaCell)
    {
        LayerMask gridLayer = hexaCell.Layer;
        
        List<HexaCell> neighborHexaCells = new List<HexaCell>();
        
        Collider[] neighborColliders = Physics.OverlapSphere(hexaCell.transform.position, 1, gridLayer);

        foreach (Collider col in neighborColliders)
        {
            HexaCell neighborCell = col.GetComponentInParent<HexaCell>();

            if (!neighborCell.IsOccupied) continue;
            if(neighborCell == hexaCell) continue;
            
            neighborHexaCells.Add(neighborCell);
        }
        
        return neighborHexaCells;
    }

    private List<HexaCell> GetSimilarNeighborCells(Material hexaCellTopMaterial, HexaCell[] neighborHexaCells)
    {
        List<HexaCell> similarNeighborHexaCells = new List<HexaCell>();

        foreach (HexaCell neighborCell in neighborHexaCells)
        {
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
            yield return new WaitForSeconds(timeGap / 2);
        }
    }

    private IEnumerator CheckCompleteStack(HexaCell hexaCell, Material topMaterial)
    {
        if (hexaCell.HexaStack.Jellies.Count < _mergeCount)
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

        if (similarHexaJellies.Count < _mergeCount)
        {
            yield break;
        }

        while (similarHexaJellies.Count > 0)
        {
            yield return null;
            similarHexaJellies[0].SetParent(null);
            hexaCell.HexaStack.Remove(similarHexaJellies[0]);
            DestroyImmediate(similarHexaJellies[0].gameObject);
            similarHexaJellies.RemoveAt(0);
            
            //Call Action
            _onScoreAdded?.Invoke();
        }
        
        updatedHexaCells.Add(hexaCell);
    }
    #endregion

    
}
