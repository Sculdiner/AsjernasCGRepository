using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Card
{
    public class ClientSideCardEvents : MonoBehaviour
    {
        public Action<ClientSideCard> OnMouseOverEventHandler;
        public Action<ClientSideCard> OnMouseExitEventHandler;

        public ClientSideCard ControllingCard;

        public void Awake()
        {
            OnMouseOverEventHandler = (c) => { };
            OnMouseExitEventHandler = (c) => { };
        }

        void OnMouseOver()
        {
            if (OnMouseOverEventHandler != null)
                OnMouseOverEventHandler(ControllingCard);
        }

        void OnMouseExit()
        {
            if (OnMouseExitEventHandler != null)
                OnMouseExitEventHandler(ControllingCard);
        }
    }
}
