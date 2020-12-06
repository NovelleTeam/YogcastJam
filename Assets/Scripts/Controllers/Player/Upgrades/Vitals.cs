using System;
using UnityEngine;

namespace Controllers.Player.Upgrades
{
    public class Vitals : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth { get; private set; }
        private bool isDead => currentHealth <= 0;
        
        public event Action DamageTaken;

        private PlayerLives _playerLives;

        private void Awake()
        {
            currentHealth = maxHealth;

            _playerLives = GetComponent<PlayerLives>();
        }

        public void TakeDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.TakeDamage used to heal! Use Vitals.HealDamage instead.");

            currentHealth -= health;

            DamageTaken?.Invoke();

            if (isDead) 
                _playerLives.Die();
        }

        public void HealDamage(int health)
        {
            if (health < 0) throw new Exception("Vitals.HealDamage used to damage! Use Vitals.TakeDamage instead.");

            currentHealth += health;

            // Health clamping
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }
}