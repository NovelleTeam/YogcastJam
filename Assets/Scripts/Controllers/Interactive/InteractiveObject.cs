using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveObject : MonoBehaviour
    {
        public bool IsTakeAble { get; protected set; }
        public bool DisableAfterTake { get; protected set; }
        public Transform destenation { get; protected set; }

        public InteractiveObject()
        {
            IsTakeAble = true;
            DisableAfterTake = true;
            destenation = null;
        }

        public virtual void Interact()
        {
        
        }
    }
}