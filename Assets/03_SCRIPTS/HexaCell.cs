using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    private HexaStack HexaStack;
    public bool IsOccupied
    {
        get => HexaStack != null;
        private set { }
    }

    public void RegisterStack(HexaStack hexaStack)
    {
        HexaStack = hexaStack;
    }
}
