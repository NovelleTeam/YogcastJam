using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveStick : InteractiveObject
    {
        public Transform destinationN;

        private void Awake()
        {
            destination = destinationN;
        }

        public InteractiveStick()
        {
            isTakeAble = true;
            isStickType = true;
        }

        public override void Interact(GameObject player)
        {
        }
    }
}