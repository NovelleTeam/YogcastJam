using DG.Tweening;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveStick : InteractiveObject
    {
        public new Transform destination;

        protected override void Start()
        {
            isTakeAble = true;
            isStickType = true;
            travelDuration = 1f;
        }

        public override void Interact(GameObject player)
        {
            transform.DOMove(destination.position, travelDuration);
        }

        public override void SyncPos()
        {
            var pos = transform;
            pos.position = destination.position;
            pos.rotation = destination.rotation;
        }
    }
}