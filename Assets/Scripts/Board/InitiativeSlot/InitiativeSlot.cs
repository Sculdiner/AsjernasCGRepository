using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeSlot : MonoBehaviour
{
    public RawImage Image;
    public ClientSideCard ReferencedCard { get; private set; }

    public void SetCardInfo(ClientSideCard card)
    {
        ReferencedCard = card;
        Image.texture = Resources.Load($"Images/{card.CardManager.Template.ImagePath}") as Texture2D;
    }
}
