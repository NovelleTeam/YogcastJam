using System;
using Controllers.Player;
using Controllers.Player.Upgrades;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Vitals))]
    [RequireComponent(typeof(PlayerInteract))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerManager : MonoBehaviour
    {
        public Material miniPlatformLitUp;
        public GameObject bigPlatform;
        [HideInInspector] public BigPlatformManager bigPlatformManager;
        [HideInInspector] public Vitals vitals;

        [SerializeField] private CameraMovement _cameraMovement;
        private PlayerInteract _playerInteract;
        private PlayerMovement _playerMovement;
        private PlayerLives _playerLives;
        private int _currentMainPlatformIndex;
        private int _nextMainPlatformIndex;
        private GameObject _currentBigPlatform;

        private void Start()
        {
            _playerInteract = GetComponent<PlayerInteract>();
            vitals = GetComponent<Vitals>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerLives = GetComponent<PlayerLives>();
            try
            {
                _currentBigPlatform = Instantiate(bigPlatform);
                bigPlatformManager = _currentBigPlatform.GetComponent<BigPlatformManager>();
            }
            catch
            {
                // ignored
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.gameObject.CompareTag("DeathFloor")) return;

            //lose life
            _playerLives.LoseLife();
            //respawn player
            _playerLives.RespawnPlayer();
        }

        public void SetCurrentMainPlatformIndex(int mainPlatformIndex)
        {
            _currentMainPlatformIndex = mainPlatformIndex;
        }

        public int GetCurrentMainPlatformIndex()
        {
            return _currentMainPlatformIndex;
        }

        public void SetNextMainPlatformIndex(int mainPlatformIndex)
        {
            _nextMainPlatformIndex = mainPlatformIndex;
        }

        public int GetNextMainPlatformIndex()
        {
            return _nextMainPlatformIndex;
        }

        public void SetPlayerRespawn(Transform newRespawn)
        {
            _playerLives.SetNewRespawn(newRespawn);
        }

        public void AddLife(int lifes)
        {
            _playerLives.GainLife(lifes);
      Debug.Log("trying to add life");
        }

        public void AddJump()
        {
            _playerMovement.AddMaxJump();
        }

        public void AddSpeed()
        {
            _playerMovement.AddMaxSpeed();
        }

        public void AddAttack()
        {
        }

        public void EnableLookAndMovement(bool enable)
        {
            _cameraMovement.enabled = enable;
            _playerMovement.enabled = enable;
        }

        public void FreezMovement()
        {
            
        }

        public void UnfreezMovement()
        {
            
        }
    }
}