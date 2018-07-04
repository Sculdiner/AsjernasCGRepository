using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseTargetingCardBehaviour : DraggingActions
{
    protected LineRenderer TargetingGizmo;
    protected string currentCardObjectName;
    protected List<ClientSideCard> ValidTargets = new List<ClientSideCard>();
    protected CardManager TargetedCard { get; private set; }
    protected abstract int Layer {get; }
    private Vector3 screenSpace;
    protected int VertexCount = 12;

    public BaseTargetingCardBehaviour(ClientSideCard card) : base(card)
    {
        TargetingGizmo = card.CardManager.TargetingGizmo;
        currentCardObjectName = card.CardStats.GeneratedCardId.ToString();
    }

    public override void OnStartDrag()
    {
        TargetingGizmo.enabled = true;
        screenSpace = Camera.main.WorldToScreenPoint(ReferencedCard.CardViewObject.transform.position);
        ReferencedCard.CardViewObject.GetComponent<DragRotator>().enabled = true;
        if (!PreDragPosition.HasValue)
        {
            PreDragPosition = ReferencedCard.CardViewObject.transform.position;
        }
        ValidTargets = GetTargetValidationMethod()(ReferencedCard);
        //TargetingGizmo.SetPosition(0, ReferencedCard.CardViewObject.transform.position + new Vector3(0, 0.4f, 0));
        //TargetingGizmo.SetPosition(1, ReferencedCard.CardViewObject.transform.position + new Vector3(0, 0.4f, 0));
    }

    public override void OnDraggingInUpdate()
    {
        #region "Gizmo"
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        var startPoint = ReferencedCard.CardViewObject.transform.position + new Vector3(0, 0.4f, 0);
        var endPoint = Camera.main.ScreenToWorldPoint(curScreenSpace);
        var midPoint = ((startPoint + new Vector3(0, 1.8f, 0)) + (endPoint + new Vector3(0, 1.8f, 0))) / 2;

        Debug.DrawLine(startPoint, midPoint, Color.green);
        Debug.DrawLine(midPoint, endPoint, Color.red);

        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio<=1; ratio +=1.0f/ VertexCount)
        {
            var tangV1 = Vector3.Lerp(startPoint, midPoint, ratio);
            var tangV2 = Vector3.Lerp(midPoint, endPoint, ratio);
            var bazier = Vector3.Lerp(tangV1, tangV2, ratio);
            pointList.Add(bazier);
        }
        TargetingGizmo.positionCount = pointList.Count;
        TargetingGizmo.SetPositions(pointList.ToArray());
        #endregion

        #region Target Aqcuisition
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 30f, Layer);

        if (hits.Length == 1)
        {
            //if you are not hitting yourself
            if (hits[0].transform.name != currentCardObjectName)
            {
                var hitCard = hits[0].transform.GetComponent<CardManager>();
                if (hitCard != null)
                {
                    if (ValidTargets.Any(s => s.CardStats.GeneratedCardId == hitCard.Template.GeneratedCardId))
                    {
                        TargetedCard = hitCard;
                        OnAcquiredNewTarget(TargetedCard);
                    }
                }
            }
        }
        else
        {
            if (TargetedCard != null)
            {
                OnLoseTarget();
                TargetedCard = null;
            }
        }
        #endregion
    }



    public override void OnEndDrag()
    {
        TargetingGizmo.enabled = false;

        if (TargetedCard != null && ValidTargets != null && ValidTargets.Any())
        {
            //If the current target is a valid target
            if (ValidTargets.Any(s => s.CardStats.GeneratedCardId == TargetedCard.Template.GeneratedCardId))
            {
                OnSuccessfullTargetAcquisition(TargetedCard);
                //clear the target (rectangle & reference)
                OnLoseTarget();
            }
            else //if the current target is not valid
            {
                OnNonSuccessfullTargetAcquisition();
            }
        }
        ReferencedCard.IsDragging = false;
        BoardManager.Instance.ActiveCard = null;
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override void KillCurrentActions()
    {
    }

    public abstract void OnAcquiredNewTarget(CardManager target);
    public abstract void OnLoseTarget();
    public abstract void OnSuccessfullTargetAcquisition(CardManager acquiredTarget);
    public abstract void OnNonSuccessfullTargetAcquisition();
    public abstract Func<ClientSideCard, List<ClientSideCard>> GetTargetValidationMethod();
}

