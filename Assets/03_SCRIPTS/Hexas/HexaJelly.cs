using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    public void MoveToStack(Vector3 targetLocalPosition, float timeGap)
    {
        Effects.DoJumpMoveFX(transform, targetLocalPosition, timeGap, out var sequence);
    }

    public void Clear()
    {
        Effects.DoMiniatureFX(transform, 1f, out var sequence);
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void DisableCollider() => _collider.enabled = false;
}
