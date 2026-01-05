using HexaSort.Utilitilies;
using UnityEngine;

namespace HexaSort.GameStateMachine.GameStates
{
    public class MainPlayGameState : BaseGameState
    {
        private float rayDistance = 500;
        
        private HexaCell targetHexaCell;
        private HexaStack currentHexaStack;
        private Quaternion currentHexaStackInitialRotation;
        private Vector3 currentHexaStackInitialPosition;
        
        public static bool IsHoldingStack = false;
        public MainPlayGameState(GameManager gameManager) : base(gameManager)
        {
            
        }
        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetMouseButtonDown(0))
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer = LayerMask.GetMask(Constants.HexagonLayer);
            Physics.Raycast(ray, out hit, rayDistance, layer);

            if (hit.collider == null)
            {
                Debug.Log("[MAINPLAY_STATE] No hexagon found");
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer = LayerMask.GetMask(Constants.GridLayer);
            Physics.Raycast(ray, out hit, rayDistance, layer);

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

            _gameManager.LastPlacedHexaCell = targetHexaCell;
            _gameManager.LevelManager.OnPlayerMoveFinished();
            
            _gameManager.ChangeState(GameState.MERGE);

            targetHexaCell = null;
            currentHexaStack = null;
        }

        private void DraggingAboveGround()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer = LayerMask.GetMask(Constants.GroundLayer);
            Physics.Raycast(ray, out hit, rayDistance, layer);

            if (hit.collider == null)
            {
                Debug.Log("[MAINPLAY_STATE] No ground found");
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

            if (!hexaCell.IsOccupied)
            {
                DraggingAboveNonOccupiedCell(hexaCell);
                return;
            }
            
            DraggingAboveGround();
            targetHexaCell = null;
        }

        private void DraggingAboveNonOccupiedCell(HexaCell hexaCell)
        {
            Vector3 currentHexaStackTargetPosition = hexaCell.transform.position + Vector3.up;
            
            currentHexaStack.transform.position = Vector3.MoveTowards(
                currentHexaStack.transform.position, 
                currentHexaStackTargetPosition, 
                Time.deltaTime * 20f);

            currentHexaStack.transform.rotation = hexaCell.transform.rotation;
            
            targetHexaCell = hexaCell;
        }
        
    }
}