using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void Awake()
    {
        StackController.OnStackPlaced += StackPlacedCallBack;
    }

    private void OnDestroy()
    {
        StackController.OnStackPlaced -= StackPlacedCallBack;
    }

    private void StackPlacedCallBack(HexaCell hexaCell)
    {
        StartCoroutine(StackPlacedCoroutine(hexaCell));
    }

    private IEnumerator StackPlacedCoroutine(HexaCell hexaCell)
    {
        yield return CheckMerge(hexaCell);
    }
    private IEnumerator CheckMerge(HexaCell hexaCell)
    {
        List<HexaCell> neighborHexaCells = GetNeighborCells(hexaCell);
        if (neighborHexaCells.Count <= 0)
        {
            Debug.Log("No neighbor hexa cells");
            yield break;
        }

        Material hexaCellTopMaterial = hexaCell.HexaStack.GetTopHexaMaterial();

        List<HexaCell> similarNeighborHexaCells = GetSimilarNeighborCells(hexaCellTopMaterial, neighborHexaCells.ToArray());
        if (similarNeighborHexaCells.Count <= 0)
        {
            Debug.Log("No similar neighbor hexa cells");
            yield break;
        }

        List<HexaJelly> hexaJelliesToAdd = GetHexaJelliesToAdd(hexaCellTopMaterial, similarNeighborHexaCells.ToArray());

        RemoveHexaJelliesFromStack(hexaJelliesToAdd, similarNeighborHexaCells.ToArray());

        yield return MoveHexaJelliesToStack(hexaCell, hexaJelliesToAdd);

        CheckCompleteStack(hexaCell, hexaCellTopMaterial);
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

    private void CheckCompleteStack(HexaCell hexaCell, Material topMaterial)
    {
        if (hexaCell.HexaStack.Jellies.Count < 10)
        {
            return;
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

        if (similarHexaJellies.Count < 10)
        {
            return;
        }

        while (similarHexaJellies.Count > 0)
        {
            similarHexaJellies[0].SetParent(null);
            hexaCell.HexaStack.Remove(similarHexaJellies[0]);
            DestroyImmediate(similarHexaJellies[0].gameObject);
            similarHexaJellies.RemoveAt(0);
        }
    }
    #endregion

    
}
