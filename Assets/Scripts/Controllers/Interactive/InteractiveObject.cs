using DG.Tweening;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveObject : MonoBehaviour
    {
        public bool isTakeAble { get; protected set; }
        public Vector3 destination;
        public float moveSpeed;
        protected float MoveDuration;

        public bool disableAfterTake { get; protected set; }

        protected int NumInteractions;
        public bool shouldLimitNumInteractions;
        public int maxNumInteractions = 1;

        protected virtual void Awake()
        {
            isTakeAble = true;
            disableAfterTake = true;

            SetDestination(destination);
        }

        // Only useful for takeable interactiveObjects.
        public void SetDestination(Vector3 position)
        {
            if (!isTakeAble) return;
            destination = position;

            MoveDuration = (transform.position - destination).magnitude / moveSpeed;
        }

        public virtual void Interact()
        {
            if (shouldLimitNumInteractions && NumInteractions >= maxNumInteractions) return;

            ++NumInteractions;

            if (isTakeAble)
                transform.DOMove(destination, MoveDuration).OnComplete(() =>
                {
                    if (disableAfterTake) gameObject.SetActive(false); // Should call Destroy here.
                });
        }
    }
}