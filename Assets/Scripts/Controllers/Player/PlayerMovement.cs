using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
    
        private void Awake()
        {
            _playerInput = new PlayerInput();
        }
        
        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}
