using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _controls;
        
        //Assingables
        private Transform _pos;
        [SerializeField] private Transform groundCheck;

        //Other
        private Rigidbody _rb;

        //Movement
        [SerializeField] private float moveSpeed = 4500;
        [SerializeField] private float maxSpeed = 20;
        private bool isGrounded => Physics.CheckSphere(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
        private bool _previousGrounded;

        //Jumping
        [SerializeField] private float jumpForce = 550f;
        [SerializeField] private int maxJumps;
        private int _alreadyJumped;

        //Input
        private float _x, _y;

        //Sliding
        private readonly Vector3 _normalVector = Vector3.up;
        private Vector3 _wallNormalVector;

        #region Setup

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _pos = transform;
            
            _controls = new PlayerInput();
            _controls.Player.Movement.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
            _controls.Player.Jump.performed += ctx => OnJump();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #endregion
        
        private void FixedUpdate()
        {
            Movement();
        }

        private void Update()
        {
            if (isGrounded && !_previousGrounded)
                _alreadyJumped = 0;
        }

        private void LateUpdate()
        {
            _previousGrounded = isGrounded;
        }

        private void OnMove(Vector2 input)
        {
            _x = input.x;
            _y = input.y;
        }

        private void OnJump()
        {
            if (_alreadyJumped >= maxJumps) return;
            Jump();
            _alreadyJumped++;
        }

        private void Movement()
        {
            //Some multipliers
            var multiplier = 1f;

            // Movement in air
            if (!isGrounded)
            {
                multiplier = 0.5f;
            }

            //Apply forces to move player
            _rb.AddForce(_pos.forward * (_y * moveSpeed * Time.deltaTime * multiplier) +
                         _pos.right * (_x * moveSpeed * Time.deltaTime * multiplier), ForceMode.Acceleration);
            
            ClampHorizontalVelocity();
        }

        private void Jump()
        {
            // Cancel vertical velocity
            var velocity = _rb.velocity;
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            _rb.velocity = velocity;

            _rb.AddForce(Vector2.up * (jumpForce * 1.5f), ForceMode.Impulse);
            _rb.AddForce(_normalVector * (jumpForce * 0.5f), ForceMode.Impulse);
        }

        private void ClampHorizontalVelocity()
        {
            var velocity = _rb.velocity;
            var horizontalVelocity = new Vector2(velocity.x, velocity.z);
            
            if (horizontalVelocity.magnitude <= maxSpeed) return;

            var clamped = horizontalVelocity.normalized * maxSpeed;
            var fallSpeed = velocity.y;
            
            _rb.velocity = new Vector3(clamped.x, fallSpeed, clamped.y);
        }
    }
}