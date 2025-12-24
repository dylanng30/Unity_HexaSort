using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Input Settings")]
    public float sensitivity = 5.0f;
    private float currentAngle;

    private void Start()
    {
        currentAngle = transform.eulerAngles.y;
    }

    private void Update()
    {
        if(StackController.IsHoldingStack || !MergeManager.FinishMerge)
            return;
        
        HandleMouseInput();
        HandleTouchInput();
    }
    private void LateUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentAngle, transform.eulerAngles.z);
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            currentAngle -= Input.GetAxis("Mouse X") * sensitivity;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentAngle += Input.GetTouch(0).deltaPosition.x * sensitivity * 0.1f; 
        }
    }
}
