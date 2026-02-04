using HexaSort.Utilities;
using UnityEngine;

namespace HexaSort.GameStateMachine.GameStates
{
    public class UseBoosterGameState : BaseGameState
    {
        public UseBoosterGameState(GameManager gameManager) : base(gameManager)
        {
            
        }
        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetMouseButtonDown(0))
            {
                HexaCell targetCell = GetHexaCell();
                
                if (targetCell != null)
                    _gameManager.BoosterController.OnHexaCellClicked(targetCell);
            }
        }

        private HexaCell GetHexaCell()
        {
            HexaCell cell = null;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask(Constants.GridLayer));
            if (hit.collider != null)
                cell = hit.collider.GetComponentInParent<HexaCell>();
            
            return cell;
        }
    }
}