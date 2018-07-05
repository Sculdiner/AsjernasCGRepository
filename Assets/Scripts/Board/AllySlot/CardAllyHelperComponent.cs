using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CardAllyHelperComponent : MonoBehaviour
{
    public ClientSideCard ReferencedCard { get; set; }

    void Start()
    {
    }

    private void OnMouseEnter()
    {
        ////Debug.Log("You moused over card");
        BoardManager.OnCursorEntersCard?.Invoke(ReferencedCard);
    }

    private void OnMouseExit()
    {
        ////Debug.Log("You exited card");
        BoardManager.OnCursorExitsCard?.Invoke(ReferencedCard);
    }
}
