using AsjernasCG.Common.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Card
{
    public class CardManager : MonoBehaviour
    {
        public GameObject Prefab;

        public Text Name;
        public Text Text;
        public Image Image;
        public Text Traits;
        public Text Power;
        public Text Health;
        public Text ResourceCost;

        public void UpdateCardView(BaseCardTemplate cardTemplate)
        {
            Text.text = cardTemplate.CardText;
            Name.text = cardTemplate.CardName;
            Traits.text = cardTemplate.Traits;
            if (Power != null && cardTemplate.Power.HasValue)
                Power.text = cardTemplate.Power.Value.ToString();
            if (Health != null && cardTemplate.Health.HasValue)
                Health.text = cardTemplate.Health.Value.ToString();
            if (ResourceCost != null && cardTemplate.BaseResourceCost.HasValue)
                ResourceCost.text = cardTemplate.BaseResourceCost.Value.ToString();
        }
    }
}
