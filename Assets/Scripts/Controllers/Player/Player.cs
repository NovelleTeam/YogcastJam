using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.Space.performed += ctx => OnSpace();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // testing input system
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnSpace()
    {
        print("wwwwww");
    }
}
