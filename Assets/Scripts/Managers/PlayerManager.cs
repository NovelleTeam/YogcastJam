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
        [HideInInspector]public Vitals vitals;

        private PlayerInteract _playerInteract;
        private PlayerMovement _playerMovement;
        private int _currentMainPlatformIndex;
        private int _nextMainPlatformIndex;
        private GameObject _currentBigPlatform;

        private void Start()
        {
            _playerInteract = GetComponent<PlayerInteract>();
            vitals = GetComponent<Vitals>();
            _playerMovement = GetComponent<PlayerMovement>();
            _currentBigPlatform = Instantiate(bigPlatform);
            bigPlatformManager = _currentBigPlatform.GetComponent<BigPlatformManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.gameObject.CompareTag("DeathFloor")) return;
            
            var pos = transform;
            pos.position = initialTransform.position;
            pos.rotation = initialTransform.rotation;
            bigPlatformManager = null;
            Destroy(_currentBigPlatform);
            _currentBigPlatform = Instantiate(bigPlatform);
            bigPlatformManager = _currentBigPlatform.GetComponent<BigPlatformManager>();
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

    private void CompletePart(int partNumber)
        {
            
        }
    }
}