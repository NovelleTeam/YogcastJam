using Controllers.Interactive;
using DG.Tweening;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        public float takeDistance = 3;

        private PlayerInput _playerInput;
        private Camera _camera;
        private InteractiveObject _interactive;

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
            _playerInput.Player.Interact.performed += ctx => CheckIfCanTake();
        }

        private void Start()
        {
            DOTween.Init();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void CheckIfCanTake()
        {
            //Will check if we looking at some object in takeDistance
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hitInfo,
                takeDistance + 10)) return;

            if (hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<InteractiveObject>() != null)
            {
                _interactive = hitInfo.collider.gameObject.GetComponent<InteractiveObject>();
                _interactive.Interact();
            }

            _interactive = null;
        }
    }
}