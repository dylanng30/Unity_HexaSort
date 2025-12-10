using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaJelly : MonoBehaviour
{
    [Header("---REFFERENCES---")]
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;

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

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void DisableCollider() => _collider.enabled = false;

}
