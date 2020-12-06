﻿using System;
using UnityEngine;

namespace Controllers.Player.Upgrades
{
    public class PlayerLives : MonoBehaviour
    {
        [SerializeField] private Transform playerRoot;

        public Transform respawnTransform;

        [SerializeField] private int startingLives;
        [SerializeField] private bool capMaxLives;
        [SerializeField] private int maxLifeCap;
        public int currentLives { get; private set; }

        private Vitals _vitals;

        public event Action LifeLost;
        public event Action GameOver;

        private void Awake()
        {
            currentLives = startingLives;

            _vitals = GetComponent<Vitals>();
            
            GameOver += OnGameOver;
        }

        private static void OnGameOver()
        {
            Debug.Log("Died lol");
        }

        public void GainLife(int lifes)
        {
            currentLives += lifes;
            if (capMaxLives && currentLives > maxLifeCap) currentLives = maxLifeCap;
        }

        private void LoseLife()
        {
            --currentLives;

            LifeLost?.Invoke();
        }

        public void Die()
        {
            LoseLife();
            
            if (currentLives > 0)
                _vitals.HealDamage(100);
            else
            {
                GameOver?.Invoke();
            }
        }

        public void SetNewRespawn(Transform respawn)
        {
            respawnTransform = respawn;
        }

        public void RespawnPlayer()
        {
            var pos = playerRoot.transform;
            pos.position = respawnTransform.position;
            pos.rotation = respawnTransform.rotation;
        }
    }
}