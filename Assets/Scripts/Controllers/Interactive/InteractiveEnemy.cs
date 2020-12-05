using Controllers.Player.Upgrades;
using UnityEngine;

namespace Controllers.Interactive
{
    [RequireComponent(typeof(Vitals))]
    public class InteractiveEnemy : InteractiveObject
    {
        protected override void Awake()
        {
            isTakeAble = false;
            disableAfterTake = false;
        }

        public override void Interact()
        {
            GetComponent<Vitals>().TakeDamage(2);
        }
    }
}