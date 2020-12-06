using Managers;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveObject : MonoBehaviour
    {
        public bool isTakeAble { get; protected set; }
        public bool isStickType { get; protected set; }
        public Transform destination { get; protected set; }
        public float travelDuration { get; protected set; }

        protected virtual void Start()
        {
            isTakeAble = true;
            isStickType = true;
            destination = null;
            travelDuration = 1f;
        }

        public virtual void Interact(GameObject player)
        {
            player.GetComponent<PlayerManager>().AddLife(1);
        }

        public virtual void SyncPos()
        {
            
        }
    }
}