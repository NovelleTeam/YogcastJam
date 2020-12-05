using System;
using UnityEngine;

namespace Controllers.Player.Upgrades
{
    public class Vitals : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        public float curHealth { get; private set; }
        public bool isDead => curHealth <= 0;

        public event Action Dead;
        public event Action DamageTaken;

        private void Awake()
        {
            curHealth = maxHealth;
        }

        public void TakeDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.TakeDamage used to heal! Use Vitals.HealDamage instead.");

            curHealth -= health;

            DamageTaken?.Invoke();

            if (!isDead) return;
            curHealth = 0;
            Dead?.Invoke();
        }

        public void HealDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.HealDamage used to damage! Use Vitals.TakeDamage instead.");

            curHealth += health;
        }
    }
}