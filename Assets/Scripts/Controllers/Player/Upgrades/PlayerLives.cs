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
        public int StartingLives => startingLives;
        public int currentLives { get; private set; }

        public Action ONLoseLife;
        public Action ONGameOver;

        private void Awake()
        {
            currentLives = StartingLives;
        }

        public void GainLife()
        {
            ++currentLives;
            if (capMaxLives && currentLives > maxLifeCap) currentLives = maxLifeCap;
        }

        public void LoseLife()
        {
            --currentLives;

            ONLoseLife?.Invoke();

            if (currentLives <= 0)
            {
                currentLives = 0;
                ONGameOver?.Invoke();
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