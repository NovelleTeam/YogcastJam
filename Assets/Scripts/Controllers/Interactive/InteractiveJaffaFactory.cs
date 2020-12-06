using Controllers.Player.Upgrades;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveJaffaFactory : InteractiveObject
    {
        protected override void Awake()
        {
            isTakeAble = false;
            isStickType = false;
            destination = null;
        }
        
        public override void Interact(GameObject player)
        {
            var vitals = player.GetComponent<Vitals>();

            vitals.HealDamage(vitals.maxHealth - vitals.curHealth);
            Debug.Log("Healed!");
        }
    }
}