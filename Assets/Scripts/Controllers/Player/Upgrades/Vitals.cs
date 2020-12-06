using System;
using UnityEngine;

namespace Controllers.Player.Upgrades
{
    public class Vitals : MonoBehaviour
    {
        public int maxHealth;
        public int curHealth { get; private set; }
        private bool isDead => curHealth <= 0;

        public event Action Dead;
        public event Action DamageTaken;

        private void Awake()
        {
            curHealth = maxHealth;
            
            Dead += OnDead;
            Dead += gameObject.GetComponent<PlayerLives>().LoseLife;
        }

        private void OnDead()
        {
            HealDamage(maxHealth - curHealth);
        }

        public void TakeDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.TakeDamage used to heal! Use Vitals.HealDamage instead.");

            curHealth -= health;

            DamageTaken?.Invoke();

            if (isDead) 
                Dead?.Invoke();
        }

        public void HealDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.HealDamage used to damage! Use Vitals.TakeDamage instead.");

            curHealth += health;

            if (curHealth > maxHealth)
                curHealth = maxHealth;
        }
    }
}