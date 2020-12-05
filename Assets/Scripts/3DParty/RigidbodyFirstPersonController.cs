using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace _3DParty
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings
        {
            public float forwardSpeed = 8.0f; // Speed when walking forward
            public float backwardSpeed = 4.0f; // Speed when walking backwards
            public float strafeSpeed = 4.0f; // Speed when walking sideways
            public float runMultiplier = 2.0f; // Speed when sprinting
            public KeyCode runKey = KeyCode.LeftShift;
            public float jumpForce = 30f;

            public AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f),
                new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

            [HideInInspector] public float currentTargetSpeed = 8f;

#if !MOBILE_INPUT
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero) return;
                if (input.x > 0 || input.x < 0)
                    //strafe
                    currentTargetSpeed = strafeSpeed;
                if (input.y < 0)
                    //backwards
                    currentTargetSpeed = backwardSpeed;
                if (input.y > 0)
                    //forwards
                    //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                    currentTargetSpeed = forwardSpeed;
#if !MOBILE_INPUT
                if (Input.GetKey(runKey)) currentTargetSpeed *= runMultiplier;
#endif
            }

#if !MOBILE_INPUT
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float
                groundCheckDistance =
                    0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )

            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air

            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float
                shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();


        private Rigidbody _rb;
        private CapsuleCollider _capsule;
        private float _yRotation;
        private Vector3 _groundContactNormal;
        private bool _jump, _previouslyGrounded, _jumping, _isGrounded;


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init(transform, cam.transform);
        }


        private void Update()
        {
            RotateView();

            if (CrossPlatformInputManager.GetButtonDown("Jump") && !_jump) _jump = true;
        }


        private void FixedUpdate()
        {
            GroundCheck();
            var input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) &&
                (advancedSettings.airControl || _isGrounded))
            {
                // always move along the camera forward as it is the direction that it being aimed at
                var camTransform = cam.transform;
                var desiredMove = camTransform.forward * input.y + camTransform.right * input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, _groundContactNormal).normalized;

                desiredMove.x *= movementSettings.currentTargetSpeed;
                desiredMove.z *= movementSettings.currentTargetSpeed;
                desiredMove.y *= movementSettings.currentTargetSpeed;
                if (_rb.velocity.sqrMagnitude <
                    movementSettings.currentTargetSpeed * movementSettings.currentTargetSpeed)
                    _rb.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
            }

            if (_isGrounded)
            {
                _rb.drag = 5f;

                if (_jump)
                {
                    _rb.drag = 0f;
                    var velocity = _rb.velocity;
                    velocity = new Vector3(velocity.x, 0f, velocity.z);
                    _rb.velocity = velocity;
                    _rb.AddForce(new Vector3(0f, movementSettings.jumpForce, 0f), ForceMode.Impulse);
                    _jumping = true;
                }

                if (!_jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon &&
                    _rb.velocity.magnitude < 1f) _rb.Sleep();
            }
            else
            {
                _rb.drag = 0f;
                if (_previouslyGrounded && !_jumping) StickToGroundHelper();
            }

            _jump = false;
        }


        private float SlopeMultiplier()
        {
            var angle = Vector3.Angle(_groundContactNormal, Vector3.up);
            return movementSettings.slopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            if (!Physics.SphereCast(transform.position, _capsule.radius * (1.0f - advancedSettings.shellOffset),
                Vector3.down, out var hitInfo,
                _capsule.height / 2f - _capsule.radius +
                advancedSettings.stickToGroundHelperDistance, Physics.AllLayers,
                QueryTriggerInteraction.Ignore)) return;
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                _rb.velocity = Vector3.ProjectOnPlane(_rb.velocity, hitInfo.normal);
        }


        private Vector2 GetInput()
        {
            var input = new Vector2
            {
                x = CrossPlatformInputManager.GetAxis("Horizontal"),
                y = CrossPlatformInputManager.GetAxis("Vertical")
            };
            movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            var pos = transform;
            var oldYRotation = pos.eulerAngles.y;

            mouseLook.LookRotation(pos, cam.transform);

            if (_isGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                var velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                _rb.velocity = velRotation * _rb.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            _previouslyGrounded = _isGrounded;
            if (Physics.SphereCast(transform.position, _capsule.radius * (1.0f - advancedSettings.shellOffset),
                Vector3.down, out var hitInfo,
                _capsule.height / 2f - _capsule.radius + advancedSettings.groundCheckDistance,
                Physics.AllLayers,
                QueryTriggerInteraction.Ignore))
            {
                _isGrounded = true;
                _groundContactNormal = hitInfo.normal;
            }
            else
            {
                _isGrounded = false;
                _groundContactNormal = Vector3.up;
            }

            if (!_previouslyGrounded && _isGrounded && _jumping) _jumping = false;
        }
    }
}