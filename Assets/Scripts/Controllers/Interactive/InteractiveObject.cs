using Managers;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveObject : MonoBehaviour
    {
        public bool isTakeAble { get; protected set; }
        public bool isStickType { get; protected set; }
        public Transform destination { get; protected set; }

        protected virtual void Awake()
        {
            isTakeAble = true;
            isStickType = true;
            destination = null;
        }

        public virtual void Interact(GameObject player)
        {
            player.GetComponent<PlayerManager>().AddLife(1);
        }
    }
}