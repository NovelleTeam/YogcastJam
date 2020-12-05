using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveStick : InteractiveObject
    {
        public Transform destenationN;

        private void Awake()
        {
            destenation = destenationN;
        }

        public InteractiveStick()
        {
            IsTakeAble = true;
            isStickType = true;
        }

        public override void Interact()
        {
            
        }
    }
}