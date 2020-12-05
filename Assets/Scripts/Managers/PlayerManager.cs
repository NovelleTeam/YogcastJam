using System;
using Controllers.Player;
using Controllers.Player.Upgrades;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Vitals))]
    [RequireComponent(typeof(PlayerInteract))]
    public class PlayerManager : MonoBehaviour
    {
        public Transform initialTransform;
        public BigPlatformManager bigPlatformManager;
        
        private PlayerInteract _playerInteract;
        private Vitals _vitals;
        private PlayerMovement _playerMovement;
        

        private void Start()
        {
            _playerInteract = GetComponent<PlayerInteract>();
            _vitals = GetComponent<Vitals>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.tag == "DethFloor")
            {
                transform.position = initialTransform.position;
                transform.rotation = initialTransform.rotation;
            }
        }

        void CompletePart(int partNumber)
        {
            
        }
    }
}