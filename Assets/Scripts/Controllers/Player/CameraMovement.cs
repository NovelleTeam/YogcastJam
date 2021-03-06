﻿using UnityEngine;

namespace Controllers.Player
{
    public class CameraMovement : MonoBehaviour
    {
        private PlayerInput _controls;

        // Transforms
        private Transform _cameraHolder;
        private Transform _player;

        // Values needed to move the camera
        private Vector2 _input;

        private float pitch => _input.y * yMultiplier * sensitivity * Time.deltaTime;
        private float yaw => _input.x * xMultiplier * sensitivity * Time.deltaTime;

        private float _xRotation;

        [SerializeField] private float sensitivity;
        [SerializeField] private float xMultiplier;
        [SerializeField] private float yMultiplier;

        #region Setup

        private void Awake()
        {
            _controls = new PlayerInput();
            _controls.Player.Look.performed += ctx => OnLook(ctx.ReadValue<Vector2>());
        }

        private void Start()
        {
            _cameraHolder = transform;

            Cursor.lockState = CursorLockMode.Locked;
            _player = _cameraHolder.parent;
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

        private void Update()
        {
            Look();
        }

        #region Input Event Functions

        private void OnLook(Vector2 input)
        {
            _input = input;
        }

        #endregion

        private void Look()
        {
            _xRotation -= pitch;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _cameraHolder.localRotation = Quaternion.Euler(Vector3.right * _xRotation);
            _player.Rotate(Vector3.up * yaw);
        }
    }
}