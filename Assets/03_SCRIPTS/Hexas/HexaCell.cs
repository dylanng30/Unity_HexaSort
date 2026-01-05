using System.Collections;
using System.Collections.Generic;
using HexaSort.Utilitilies;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public int Q;
    public int R;
    public int S;
    
    public Vector3Int Coordinates => new Vector3Int(Q, R, S);
    
    public LayerMask Layer
    {
        get => LayerMask.GetMask(Constants.GridLayer);
    }
    public HexaStack HexaStack {get; private set;}

    public void Setup(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }
    
    public bool IsOccupied
    {
        get => HexaStack != null;
    }

    public void RegisterStack(HexaStack hexaStack)
    {
        HexaStack = hexaStack;
    }

    public void Clear()
    {
        if (HexaStack == null)
            return;
        
        Destroy(HexaStack.gameObject);
        HexaStack = null;
    }
}
