using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveStick : InteractiveObject
    {
        public Transform destenationN;

        private void Awake()
        {
            destination = destenationN;
        }

        public InteractiveStick()
        {
            isTakeAble = true;
            isStickType = true;
        }

        public override void Interact()
        {
            
        }
    }
}