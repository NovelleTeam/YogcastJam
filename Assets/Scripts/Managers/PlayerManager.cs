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
        public Material miniPlatformLitUp;
        public GameObject bigPlatform;
        [HideInInspector] public BigPlatformManager bigPlatformManager;

        private PlayerInteract _playerInteract;
        private Vitals _vitals;
        private PlayerMovement _playerMovement;
        private GameObject _currentPlatform;

        private void Start()
        {
            _playerInteract = GetComponent<PlayerInteract>();
            _vitals = GetComponent<Vitals>();
            _playerMovement = GetComponent<PlayerMovement>();
            _currentPlatform = Instantiate(bigPlatform);
            bigPlatformManager = _currentPlatform.GetComponent<BigPlatformManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.gameObject.CompareTag("DeathFloor")) return;
            
            var pos = transform;
            pos.position = initialTransform.position;
            pos.rotation = initialTransform.rotation;
            bigPlatformManager = null;
            Destroy(_currentPlatform);
            _currentPlatform = Instantiate(bigPlatform);
            bigPlatformManager = _currentPlatform.GetComponent<BigPlatformManager>();
        }

        private void CompletePart(int partNumber)
        {
        }
    }
}