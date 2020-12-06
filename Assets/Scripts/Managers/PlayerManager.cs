using System.Collections;
using Controllers.Player;
using Controllers.Player.Upgrades;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private Rigidbody _rigidbody;
        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _rigidbody = GetComponent<Rigidbody>();
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

            vitals.TakeDamage(50);
            Debug.Log("Health: " + vitals.currentHealth);
            
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

        public void FreezeMovement()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public void UnfreezeMovement()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}












