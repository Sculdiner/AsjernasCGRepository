using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Card
{
    public class ClientSideCardEvents : MonoBehaviour
    {
        public Action OnMouseOverEventHandler;
        public Action OnMouseExitEventHandler;

        public void Awake()
        {
            OnMouseOverEventHandler = () => { };
            OnMouseExitEventHandler = () => { };
        }

        void OnMouseOver()
        {
            if (OnMouseOverEventHandler != null)
                OnMouseOverEventHandler();
        }

        void OnMouseExit()
        {
            if (OnMouseExitEventHandler != null)
                OnMouseExitEventHandler();
        }
    }
}
