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
        float arcHeight = 1f;
        Vector3 angleRotation = GetAngleRotation(transform.localPosition, targetLocalPosition);
        transform.DOLocalRotate(angleRotation, timeGap);
        transform.DOLocalJump(targetLocalPosition, arcHeight, 1, timeGap).SetEase(Ease.Linear);
    }

    public void DisableCollider() => _collider.enabled = false;

    private Vector3 GetAngleRotation(Vector3 currentLocalPostion, Vector3 targetLocalPosition)
    {
        Vector3 direction = targetLocalPosition - currentLocalPostion;
        direction.y = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        return new Vector3(0, 0, angle);
    }
}
