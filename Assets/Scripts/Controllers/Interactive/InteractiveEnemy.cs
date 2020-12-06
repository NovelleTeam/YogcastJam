using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveEnemy : InteractiveObject
    {
        public InteractiveEnemy()
        {
            isTakeAble = false;
        }

        public override void Interact(GameObject player)
        {
        }
    }
}