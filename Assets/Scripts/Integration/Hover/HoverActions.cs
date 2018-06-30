using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class HoverActions
{
    public ClientSideCard Card { get; private set; }
    public HoverActions(ClientSideCard card)
    {
        Card = card;
    }
    public Vector3? HoverPosition;
    public Quaternion? HoverRotation;
    public Vector3? PreHoverPosition;
    public Quaternion? PreHoverRotation;

    public abstract void OnHoverStart();
    public abstract void OnHoverEnd();
    public abstract void OnImmediateKill();
    public void StoreHoverPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        HoverPosition = position;
        HoverRotation = rotation;
    }

    public void StorePreHoverPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        PreHoverPosition = position;
        PreHoverRotation = rotation;
    }
}