using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestHoverBehaviour : HoverActions
{
    public QuestHoverBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override void OnHoverEnd()
    {
        Card.CardManager.VisualStateManager.EndPreviewAndRetainOriginalState();
    }

    public override void OnHoverStart()
    {
        var viewportPoint = Camera.main.WorldToViewportPoint(Card.CardManager.transform.position + new Vector3(2f, 0, -0.5f));
        Card.CardManager.VisualStateManager.PreviewAndRetainOriginalState();
        Card.CardManager.VisualStateManager.Preview.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPoint.x, viewportPoint.y, 4f));
        //var plane = GameObject.Find("HoverHelperPlane");
        //plane.GetComponent<BoxCollider>().enabled = true;
        //var cardToScreen = Camera.main.WorldToScreenPoint(Card.CardViewObject.transform.position);
        //var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cardToScreen.z);
        ////convert the screen mouse position to world point
        //var ray = new Ray(Camera.main.ScreenToWorldPoint(curScreenSpace), Card.CardViewObject.transform.position);
        //var hits = Physics.RaycastAll(ray);

        //var asd = "";
        //plane.GetComponent<BoxCollider>().enabled = false;
        //Debug.Log($"started hover on {Card.CardStats.CardName}. CardToScreen: {curPosition.x}/{curPosition.y}/{curPosition.z}");
    }

    public override void OnImmediateKill()
    {
        Card.CardManager.VisualStateManager.EndPreviewAndRetainOriginalState();
    }
}
