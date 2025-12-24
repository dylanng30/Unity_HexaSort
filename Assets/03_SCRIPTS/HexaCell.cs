using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public LayerMask Layer
    {
        get => LayerMask.GetMask(Constants.GridLayer);
    }
    public HexaStack HexaStack {get; private set;}
    
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
