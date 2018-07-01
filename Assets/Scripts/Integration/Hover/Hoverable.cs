using AsjernasCG.Common.BusinessModels.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class Hoverable : MonoBehaviour
{
    public ClientSideCard ControllingCard;
    private HoverActions actions;
    
    public void SetAction<T>() where T : HoverActions
    {
        var action = (T)Activator.CreateInstance(typeof(T), ControllingCard);
        actions = action;
    }

    public void OnMouseEnter()
    {
        actions?.OnHoverStart();
    }

    public void OnMouseExit()
    {
        actions?.OnHoverEnd();
    }

    public void ForceKillHover()
    {
        actions?.OnImmediateKill();
    }


    public void ForceHoveringPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        actions?.StoreHoverPositionAndRotation(position, rotation);
    }

    public void ForcePreHoveringPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        actions?.StorePreHoverPositionAndRotation(position, rotation);
    }
}
