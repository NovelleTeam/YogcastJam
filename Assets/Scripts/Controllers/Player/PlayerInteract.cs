using System.Collections;
using Controllers.Interactive;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(PlayerManager))]
    public class PlayerInteract : MonoBehaviour
    {
        public float takeDistance = 3;

        private PlayerInput _playerInput;
        private Camera _camera;
        private InteractiveObject _interactive;
        private PlayerManager _playerManager;

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
            _playerInput.Player.Interact.performed += ctx => OnInteract();
            _playerManager = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnInteract()
        {
            try
            {
                _interactive = GetObjectAtRaycast().GetComponent<InteractiveObject>();
            }
            catch
            {
                return;
            }

            if (_interactive == null) return;
            
            _interactive.Interact(gameObject);

            if (_interactive.isStickType)
                StartCoroutine(WaitForInteract(_interactive));

            _interactive = null;
        }

        private GameObject GetObjectAtRaycast()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hitInfo,
                takeDistance)) return null;
            return hitInfo.collider != null ? hitInfo.collider.gameObject : null;
        }

        private IEnumerator WaitForInteract(InteractiveObject interactiveObj)
        {
            yield return new WaitForSeconds(interactiveObj.travelDuration);
            
            interactiveObj.SyncPos();

            _playerManager.bigPlatformManager.MakeSuprise();
            
            Destroy(interactiveObj.gameObject.GetComponent<Rigidbody>());
            Destroy(interactiveObj);
            
            Debug.Log("In place!");
        }
    }
}