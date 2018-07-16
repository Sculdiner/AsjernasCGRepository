using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BoardHoverBehaviour : HoverActions
{
    public BoardHoverBehaviour(ClientSideCard card) : base(card)
    {
    }

    public override void OnHoverEnd()
    {
        Card.CardManager.VisualStateManager.EndPreviewAndRetainOriginalState();
    }

    public override void OnHoverStart()
    {

        var offsetVector = new Vector3();
        switch (Card.CardStats.CardType)
        {
            case AsjernasCG.Common.BusinessModels.CardModels.CardType.Character:
                offsetVector = new Vector3(0f, 0f, 2f);
                break;
            case AsjernasCG.Common.BusinessModels.CardModels.CardType.Equipment:
                offsetVector = new Vector3(0f, 0f, 2f);
                break;
            case AsjernasCG.Common.BusinessModels.CardModels.CardType.Ability:
                offsetVector = new Vector3(0f, 0f, 2f);
                break;
            case AsjernasCG.Common.BusinessModels.CardModels.CardType.Follower:
                offsetVector = new Vector3(1.5f, 0, 0.2f);
                break;
            case AsjernasCG.Common.BusinessModels.CardModels.CardType.Minion:
                offsetVector = new Vector3(1.5f, 0, 0.2f);
                break;
            default:
                break;
        }


        var viewportPoint = Camera.main.WorldToViewportPoint(Card.CardManager.VisualStateManager.CurrentState.transform.position + offsetVector);
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
