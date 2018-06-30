using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseTargetingCardBehaviour : DraggingActions
{
    public ClientSideCard ControllingCard { get; set; }
    private GameObject Card { get; set; }
    private Vector3 screenSpace;
    protected LineRenderer Lr { get; set; }
    public BaseTargetingCardBehaviour(ClientSideCard card) : base(card)
    {
        ControllingCard = card;
        Card = card.CardViewObject;
        Lr = Card.GetComponentInChildren<LineRenderer>();
    }

    public override bool DragSuccessful()
    {
        return true;
    }

    public override void KillCurrentActions()
    {

    }

    public override void OnDraggingInUpdate()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
        Lr.SetPositions(new Vector3[] { curPosition });
    }

    public override void OnEndDrag()
    {
        RaycastHit[] hits;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, 30f, LayerMask.GetMask("RaycastEligibleTargets"));
    }

    public override void OnStartDrag()
    {
        ControllingCard.IsUnderPlayerControl = true;
        screenSpace = Camera.main.WorldToScreenPoint(Card.transform.position);
        ApplyDelayAndDissolve();

    }

    //this may be handled by the effect itself instead
    private IEnumerator ApplyDelayAndDissolve()
    {
        yield return new WaitForSeconds(1f);
        Card.transform.Find("VisualObject").gameObject.SetActive(false);
        Card.transform.Find("PreviewObject").gameObject.SetActive(false);
        Card.transform.Find("Target").gameObject.SetActive(true);
    }
}

