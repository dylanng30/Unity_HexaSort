using HexaSort.Boosters;
using HexaSort.Utilitilies;
using UnityEngine;

namespace HexaSort
{
    public enum InputState
    {
        None,
        
        
        //Boosters
        UsingBooster,
    }
    public class InputManager : PersistentSingleton<InputManager>
    {
        [SerializeField] private LayerMask gridLayer;
        public InputState CurrentState { get; private set; }
        public void SetState(InputState state)
        {
            CurrentState = state;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500, gridLayer);
                
                if (hit.collider == null)
                {
                    Debug.Log("[STACK CONTROLLER] No hexagon found");
                    return;
                }
                
                HexaCell hexaCell = hit.collider.GetComponentInParent<HexaCell>();
                if (!hexaCell.IsOccupied)
                {
                    return;
                }
                
                BoosterManager.Instance.OnHexaCellClicked(hexaCell);
            }
        }
        
    }
}