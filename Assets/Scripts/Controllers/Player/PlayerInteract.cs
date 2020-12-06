using System.Collections;
using Controllers.Interactive;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(PlayerManager))]
    public class PlayerInteract : MonoBehaviour
    {
        public float takeDistance = 3;
        public Transform interactiveObjectDestination;
        public float interactiveObjectTravelDuration = 1;

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

            if (CanTake())
            {
                interactiveObjectDestination = _interactive.destination;
                _interactive.gameObject.transform.DOMove(interactiveObjectDestination.position,
                    interactiveObjectTravelDuration);

                _interactive.Interact(gameObject);

                if (_interactive.isStickType)
                    StartCoroutine(WaitForInteract(_interactive));

                _interactive = null;
            }
            else if (_interactive != null)
            {
                _interactive.Interact(gameObject);
                _interactive = null;
            }
        }

        private GameObject GetObjectAtRaycast()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hitInfo,
                takeDistance)) return null;
            return hitInfo.collider != null ? hitInfo.collider.gameObject : null;
        }

        private bool CanTake()
        {
            return _interactive != null &&
                   Vector3.SqrMagnitude(_interactive.transform.position - transform.position) <=
                   takeDistance * takeDistance && _interactive.isTakeAble && _interactive.destination != null;
        }

        private IEnumerator WaitForInteract(Component interactiveObj)
        {
            yield return new WaitForSeconds(interactiveObjectTravelDuration);
            var objTransform = interactiveObj.transform;
            objTransform.position = interactiveObjectDestination.position;
            objTransform.rotation = interactiveObjectDestination.rotation;
            _playerManager.bigPlatformManager.MakeSuprise();
            Destroy(interactiveObj.gameObject.GetComponent<Rigidbody>());
            Destroy(interactiveObj);
        }
    }
}