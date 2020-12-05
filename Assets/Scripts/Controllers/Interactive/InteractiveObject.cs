using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveObject : MonoBehaviour
    {
        public bool isTakeAble { get; protected set; }
        public bool isStickType { get; protected set; }
        public Transform destination { get; protected set; }

        public InteractiveObject()
        {
            isTakeAble = true;
            isStickType = true;
            destination = null;
        }

        public virtual void Interact()
        {
        }
    }
}