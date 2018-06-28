using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Integration.DragBehaviour
{
    //FollowerCastDragBehaviour
    //FollowerBoardDragBehaviour
    public class FollowerCastDragBehaviour : DraggingActions
    {
        public int Layer => LayerMask.NameToLayer("AllyPlayArea");
        public GameObject TargetedArea { get; private set; }
        public ClientSideCard ClientSideCard;
        private AllySlotManager PlacementTarget;

        public FollowerCastDragBehaviour(ClientSideCard card) : base(card)
        {
        }

        public override void OnDraggingInUpdate()
        {
            //Debug.DrawLine(transform.position, t, Color.green);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            var hit = Physics.Raycast(ray, out hitObject, 30f, Layer);

            if (TargetedArea != null)
            {
                Debug.DrawLine(ray.origin, TargetedArea.transform.position, Color.magenta);
            }

            //getcomponent ally play area to find the play area manager and it's owner
            if (hit)
            {
                var slotComponent = hitObject.transform.gameObject.GetComponent<AllySlotManager>();
                if (slotComponent.IsCurrentPlayerArea())
                {
                    PlacementTarget = slotComponent;
                    return;
                }
            }
            //Debug.Info("asdasd");
            PlacementTarget = null;
            //var card = BoardManager.GetCard(hits[i]);
            //if (card != null && card.CardStats.GeneratedCardId != ControllingCard.CardStats.GeneratedCardId)
            //{
            //    if (TargetedCard != card)
            //    {
            //        //mouse enter
            //    }
            //    //IsOverATarget = true;
            //    TargetedCard = card;
            //    Debug.Log("hit card " + TargetedCard.CardStats.GeneratedCardId);
            //}
            //if (TargetedCard != null)
            //{
            //    //mouse exit
            //    Debug.Log("exited card " + TargetedCard.CardStats.GeneratedCardId);
            //    TargetedCard = null;
            //}
        }

        public override void OnEndDrag()
        {
            if (PlacementTarget != null)
            {
                PlacementTarget.AddAllyCardLast(ClientSideCard);
            }
        }

        public override void OnStartDrag()
        {
        }

        public override bool DragSuccessful()
        {
            //what are thoooooooose
            return true;
        }

        public override void KillCurrentActions()
        {

        }
    }
}
