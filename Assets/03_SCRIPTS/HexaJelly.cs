using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaJelly : MonoBehaviour
{
    [Header("---REFFERENCES---")]
    [SerializeField] private new Renderer _renderer;

    public HexaStack HexaStack {get; private set;}

    public Material Material
    {
        get => _renderer.material;
        set => _renderer.material = value;
    }

    public void RegisterStack(HexaStack hexaStack)
    {
        HexaStack = hexaStack;
    }
    
}
