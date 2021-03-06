﻿using UnityEngine;

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
        [SerializeField] public float moveSpeed;
        [SerializeField] public float maxSpeed;
        [SerializeField] public float counterMovement;
        [SerializeField] public float sprintModifier;
        private bool isGrounded => Physics.CheckSphere(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
        private bool _previousGrounded;

        //Jumping
        [SerializeField] public float jumpForce;
        [SerializeField] public int maxJumps;
        private int _alreadyJumped;

        //Input
        private float _x, _y;
        private float _previousX, _previousY;

        //Sliding
        private readonly Vector3 _normalVector = Vector3.up;
        private Vector3 _wallNormalVector;
        
        // audio manager
        private AudioManager _audioManager;

        #region Setup
        
        private void Awake()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _rb = GetComponent<Rigidbody>();
            _pos = transform;

            _controls = new PlayerInput();
            _controls.Player.Movement.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
            _controls.Player.Jump.performed += ctx => OnJump();
            _controls.Player.SprintEnter.performed += ctx => OnSprintEnter();
            _controls.Player.SprintExit.performed += ctx => OnSprintExit();
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
            _audioManager.Play("START");
        }

        #endregion

        private void FixedUpdate()
        {
            Movement();

            _previousX = _x;
            _previousY = _y;
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

        #region Input Event Functions

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

        private void OnSprintEnter()
        {
            maxSpeed += sprintModifier;
        }

        private void OnSprintExit()
        {
            maxSpeed -= sprintModifier;
        }

        #endregion

        private void Movement()
        {
            //Some multipliers
            var multiplier = 1f;

            // Movement in air
            if (!isGrounded)
                multiplier = 0.5f;
            else if (!IsIce())
                CounterMovement();

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
            _audioManager.Play("JUMP" + Random.Range(1, 5).ToString());
            print("JUMP" + Random.Range(1, 5).ToString());
            _rb.AddForce(Vector2.up * (jumpForce * 1.5f), ForceMode.Impulse);
            _rb.AddForce(_normalVector * (jumpForce * 0.5f), ForceMode.Impulse);
        }

        private void CounterMovement()
        {
            var vector = new Vector3();

            if (_x == 0f && _previousX != 0f)
                vector += _pos.right * -_previousX;
            if (_y == 0f && _previousY != 0f)
                vector += _pos.forward * -_previousY;

            _rb.AddForce(vector * counterMovement, ForceMode.Acceleration);
        }

        private bool IsIce()
        {
            return Physics.Raycast(groundCheck.position, -groundCheck.up, out var hitInfo, 0.4f) && hitInfo.collider.gameObject.CompareTag("Ice");
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

        public void AddMaxJump()
        {
            maxJumps += 1;
        }

        public void AddMaxSpeed()
        {
            maxSpeed += 1f;
        }
    }
}