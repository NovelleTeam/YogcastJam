using System;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveStick : interactiveObject
    {
        public Transform destenationN;

        private void Awake()
        {
            destenation = destenationN;
        }

        public InteractiveStick()
        {
            IsTakeAble = true;
            DisableAfterTake = false;
        }

        public override void Interact()
        {
            
        }
    }
}