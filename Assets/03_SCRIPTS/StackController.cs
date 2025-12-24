using System;
using System.Collections;
using System.Collections.Generic;
using HexaSort;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    [Header("---SETTINGS---")]
    [SerializeField] private LayerMask hexagonLayer;
    [SerializeField] private LayerMask gridLayer;
    [SerializeField] private LayerMask groundLayer;
    
    [Space(10)]
    [SerializeField] private float rayDistance;
    
    [Header("---DATA---")]
    private HexaCell targetHexaCell;

    [Header("---ACTIONS---")]
    public static Action<HexaCell> OnStackPlaced;

    private HexaStack currentHexaStack;
    
    private Quaternion currentHexaStackInitialRotation;
    private Vector3 currentHexaStackInitialPosition;

    public static bool IsHoldingStack = false;
    
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if(_gameManager.CurrentState != GameState.PLAYING)
            return;
        
        if (Input.GetMouseButtonDown(0) && MergeManager.FinishMerge)
        {
            MouseDown();
        }
        else if(Input.GetMouseButton(0) && currentHexaStack != null)
        {
            MouseDrag();
        }
        else if (Input.GetMouseButtonUp(0) && currentHexaStack != null)
        {
            IsHoldingStack = false;
            MouseUp();
        }
    }

    private void MouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance, hexagonLayer);

        if (hit.collider == null)
        {
            Debug.Log("[STACK CONTROLLER] No hexagon found");
            return;
        }
        
        var hexaJelly = hit.collider.GetComponentInParent<HexaJelly>();
        if (hexaJelly == null) return;
        
        IsHoldingStack = true;
        currentHexaStack = hexaJelly.HexaStack;
        currentHexaStackInitialPosition = currentHexaStack.transform.position;
        currentHexaStackInitialRotation = currentHexaStack.transform.rotation;
    }

    private void MouseDrag()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance, gridLayer);

        if (hit.collider == null)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveGrid(hit);
        }
    }

    private void MouseUp()
    {
        if (targetHexaCell == null)
        {
            currentHexaStack.transform.position = currentHexaStackInitialPosition;
            currentHexaStack.transform.rotation = currentHexaStackInitialRotation;
            currentHexaStack = null;
            return;
        }
        
        currentHexaStack.transform.position = targetHexaCell.transform.position + new Vector3(0, 0.25f, 0);
        currentHexaStack.transform.SetParent(targetHexaCell.transform);
        
        currentHexaStack.transform.localRotation = Quaternion.identity;
        
        targetHexaCell.RegisterStack(currentHexaStack);
        currentHexaStack.Place();

        OnStackPlaced?.Invoke(targetHexaCell);

        targetHexaCell = null;
        currentHexaStack = null;
    }

    private void DraggingAboveGround()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayDistance, groundLayer);

        if (hit.collider == null)
        {
            Debug.Log("[STACK CONTROLLER] No ground found");
            return;
        }
        
        //Debug.Log("[STACK CONTROLLER] DraggingAboveGround");
        targetHexaCell = null;
        Vector3 currentHexaStackTargetPosition = hit.point + Vector3.up;
        
        currentHexaStack.transform.position = Vector3.MoveTowards(
            currentHexaStack.transform.position, 
            currentHexaStackTargetPosition, 
            Time.deltaTime * 20f);
    }

    private void DraggingAboveGrid(RaycastHit hit)
    {
        HexaCell hexaCell = hit.collider.GetComponentInParent<HexaCell>();

        if (hexaCell.IsOccupied)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveNonOccupiedCell(hexaCell);
            return;
        }

        targetHexaCell = null;
    }

    private void DraggingAboveNonOccupiedCell(HexaCell hexaCell)
    {
        //Debug.Log("[STACK CONTROLLER] DraggingAboveNonOccupiedCell");
        Vector3 currentHexaStackTargetPosition = hexaCell.transform.position + Vector3.up;
        
        currentHexaStack.transform.position = Vector3.MoveTowards(
            currentHexaStack.transform.position, 
            currentHexaStackTargetPosition, 
            Time.deltaTime * 20f);
        
        currentHexaStack.transform.rotation = hexaCell.transform.rotation;
        
        targetHexaCell = hexaCell;
    }
}
