﻿using AsjernasCG.Common.BusinessModels.CardModels;
using DG.Tweening;
using EZCameraShake;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterEquipmentManager : PositionalSlotManager
{
    [OdinSerialize]
    public Dictionary<PlacementPosition, BoxCollider> PositionalSlots;

    private List<ClientSideCard> Equipments = new List<ClientSideCard>();
    private readonly object equipmentLocker = new object();

    public void UpdatePositions(bool newEquipmentAdded, bool callbackOnEnd)
    {
        var sequence = DOTween.Sequence();
        switch (Equipments.Count)
        {
            case 0:
                break;
            case 1:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft, newEquipmentAdded, sequence);
                break;
            case 2:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft, false, sequence);
                SetCardPositionSlot(Equipments[1], PlacementPosition.OddMiddle, newEquipmentAdded, sequence);
                break;
            case 3:
                SetCardPositionSlot(Equipments[0], PlacementPosition.OddMiddleLeft, false, sequence);
                SetCardPositionSlot(Equipments[1], PlacementPosition.OddMiddle, false, sequence);
                SetCardPositionSlot(Equipments[2], PlacementPosition.OddMiddleRight, newEquipmentAdded, sequence);
                break;
            default:
                break;
        }
        if (callbackOnEnd)
        {
            sequence.OnComplete(() =>
            {
                PhotonEngine.CompletedAction();
            });
        }
    }

    public bool AddEquipment(ClientSideCard equipment)
    {
        lock (equipmentLocker)
        {
            if (Equipments.Count < 3)
            {
                equipment.CardViewObject.GetComponent<Draggable>().SetAction<NoDragBehaviour>();
                equipment.SetLocation(CardLocation.PlayArea);
                equipment.CardViewObject.gameObject.GetComponent<DragRotator>().DisableRotator();

                equipment.CardManager.SlotManager?.RemoveSlot(equipment.CardStats.GeneratedCardId);
                equipment.CardManager.SlotManager = this;

                equipment.CardManager.VisualStateManager.ChangeVisual(CardVisualState.Equipment);
                equipment.CardViewObject.transform.position = GameObject.Find("EquipmentStart").transform.position;
                Equipments.Add(equipment);
                UpdatePositions(true, true );

                return true;
            }
            return false;
        }
    }

    public override void RemoveSlot(int equipmentGeneratedCardId)
    {
        lock (equipmentLocker)
        {
            var eq = Equipments.FirstOrDefault(s => s.CardStats.GeneratedCardId == equipmentGeneratedCardId);
            if (eq == null)
                return;
            Equipments.Remove(eq);
            eq.CardManager.VisualStateManager.ChangeVisual(CardVisualState.None);
            eq.CardViewObject.SetActive(false);
            //var boxCollider = eq.CardViewObject.GetComponent<BoxCollider>();
            //boxCollider.center = new Vector3(0, 0, 0);
            //boxCollider.size = new Vector3(1, 1, 1);
            UpdatePositions(false, false);
        }
    }

    private void SetCardPositionSlot(ClientSideCard cardManager, PlacementPosition position, bool tween, Sequence sequence)
    {
        var slot = PositionalSlots[position];
        if (tween)
        {
            sequence.Insert(0, cardManager.CardViewObject.transform.DOMove(slot.transform.position, 1f)).SetEase(Ease.InExpo);
            sequence.InsertCallback(1f, () => { CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f); });
            
        }
        else
        {
            cardManager.CardViewObject.transform.position = slot.transform.position;
        }
        var boxCollider = cardManager.CardViewObject.GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(slot.center.x, slot.center.y, slot.center.z);
        boxCollider.size = new Vector3(slot.size.x, slot.size.y, slot.size.z);
    }
}