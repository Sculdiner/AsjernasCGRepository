﻿using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;
using EZCameraShake;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterAbilityManager : PositionalSlotManager
{
    public Transform PositionSlot1;
    public Transform PositionSlot2;

    public Material DefaultAbilityMaterial;
    private readonly object abilityLocker = new object();

    public List<ClientSideCard> Abilities = new List<ClientSideCard>();

    public void UpdatePositions(bool newEquipmentAdded)
    {
        switch (Abilities.Count)
        {
            case 0:
                break;
            case 1:
                Move(Abilities[0], PositionSlot1, newEquipmentAdded);
                break;
            case 2:
                Move(Abilities[0], PositionSlot1, false);
                Move(Abilities[1], PositionSlot2, newEquipmentAdded);
                break;
            default:
                break;
        }
    }


    public void AddAbility(ClientSideCard abilityToAdd)
    {
        lock (abilityLocker)
        {
            if (Abilities.Count < 2)
            {
                abilityToAdd.CardViewObject.GetComponent<Draggable>().SetAction<NoDragBehaviour>();
                abilityToAdd.SetLocation(CardLocation.PlayArea);
                abilityToAdd.CardViewObject.gameObject.GetComponent<DragRotator>().DisableRotator();

                abilityToAdd.CardManager.SlotManager?.RemoveSlot(abilityToAdd.CardStats.GeneratedCardId);
                abilityToAdd.CardManager.SlotManager = this;


                abilityToAdd.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Ability);
                abilityToAdd.CardViewObject.transform.position = GameObject.Find("EquipmentStart").transform.position;

                Abilities.Add(abilityToAdd);
                UpdatePositions(true);
            }
        }
    }

    public void Move(ClientSideCard abilityCardManager, Transform trans, bool isNew)
    {
        if (isNew)
        {
            abilityCardManager.CardViewObject.transform.DOMove(trans.position, 1f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
                PhotonEngine.CompletedAction();
            });
        }
        else
        {
            abilityCardManager.CardViewObject.transform.position = trans.position;
            PhotonEngine.CompletedAction();
        }
    }

    public override void RemoveSlot(int cardId)
    {
        lock (abilityLocker)
        {
            var ab = Abilities.FirstOrDefault(s => s.CardStats.GeneratedCardId == cardId);
            if (ab == null)
                return;

            Abilities.Remove(ab);
            ab.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            ab.CardViewObject.SetActive(false);
            //var boxCollider = eq.CardViewObject.GetComponent<BoxCollider>();
            //boxCollider.center = new Vector3(0, 0, 0);
            //boxCollider.size = new Vector3(1, 1, 1);
            UpdatePositions(false);
        }
    }
}
