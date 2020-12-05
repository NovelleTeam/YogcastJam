using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;

        // The default input vector
        private Vector3 _input;
        
        // The ground check
        private bool isGrounded => 
            Physics.CheckSphere(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
        
        // Components
        private Rigidbody _rb;
        
        // Ground check object's Transform
        [SerializeField] private Transform groundCheck;
        
        // Number of jumps left
        private int _jumpsLeft;
        
        [Header("Values")]
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;

        [SerializeField] private float sprintModifier = 3f;

        // Maximum number of jumps (can be changed by upgrades/downgrades)
        public int maxJumps = 1;
        
        // Basic setup for the input system and all components
        #region Setup

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Movement.performed += ctx => OnMovement(ctx.ReadValue<Vector2>());
            _playerInput.Player.Jump.performed += ctx => OnJump();
            _playerInput.Player.SprintEnter.performed += ctx => OnSprintEnter();
            _playerInput.Player.SprintExit.performed += ctx => OnSprintExit();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        #endregion

        private void Update()
        {
            if (!isGrounded) return;
            _jumpsLeft = maxJumps;
            Move();
        }

        #region Input Event Functions

        private void OnMovement(Vector2 input)
        {
            _input = new Vector3(input.x, 0f, input.y);
        }

        private void OnJump()
        {
            // Jumping if possible
            if (!isGrounded || _jumpsLeft <= 0) return;
            Jump(jumpForce);
            _jumpsLeft--;
        }

        private void OnSprintEnter()
        {
            maxSpeed += sprintModifier;
        }

        private void OnSprintExit()
        {
            maxSpeed -= sprintModifier;
        }

        #endregion

        private void Move()
        {
            _rb.velocity = _input * maxSpeed + Vector3.up * _rb.velocity.y;
        }

        private void Jump(float force)
        {
            _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
}