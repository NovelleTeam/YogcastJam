using UnityEngine;

namespace Controllers.Player
{
    public class CameraMovement : MonoBehaviour
    {
        private PlayerInput _controls;
        
        // Transforms
        private Transform _cameraHolder;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private Transform player;

        [Header("Values")] 
        
        [SerializeField] private float sensitivity = 20f;
        [SerializeField] private float xMultiplier = 1.5f;
        [SerializeField] private float yMultiplier = 1f;

        #region Setup

        private void Awake()
        {
            _controls = new PlayerInput();
        }

        private void Start()
        {
            _cameraHolder = transform;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        #endregion
    }
}
