using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HexaSort.ObjectPool;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HexaJelly : MonoBehaviour
{
    [Header("---REFFERENCES---")]
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;

    public HexaStack HexaStack {get; private set;}

    private BaseObjectPool<HexaJelly> _pool;
    private Vector3 _originalScale;

    public Material Material
    {
        get => _renderer.material;
        set => _renderer.material = value;
    }
    private void Awake()
    {
        _originalScale = transform.localScale;
    }
    private void OnEnable()
    {
        ResetState();
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
        transform.DOKill();

        Effects.DoMiniatureFX(transform, 1f, out var sequence);
        sequence.OnComplete(() =>
        {
            if(_pool != null)
                _pool.Return(this);
            else
                Destroy(gameObject);
        });
    }

    public void DisableCollider() => _collider.enabled = false;

    public void RegisterPool(BaseObjectPool<HexaJelly> pool)
    {
        _pool = pool;
    }

    private void ResetState()
    {
        transform.localScale = _originalScale;
        if (_collider != null)
            _collider.enabled = true;
        transform.SetParent(null);
        transform.localRotation = Quaternion.identity;
        HexaStack = null;
    }
}
