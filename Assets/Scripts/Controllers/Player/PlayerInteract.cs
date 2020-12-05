﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float takeDistance = 3;
    
    private PlayerInput _playerInput;
    private Camera _camera;
    private interactiveObject _interactive;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        _playerInput = new PlayerInput();
        _playerInput.Player.Interact.performed += ctx => CheckIfCanTake();
    }

    private void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        RaycastHit hitInfo;

        //Will check if we looking at some object in takeDistance
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, takeDistance + 10))
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<interactiveObject>() != null)
            {
                _interactive = hitInfo.collider.gameObject.GetComponent<interactiveObject>();
                _interactive.Interact();
                _interactive = null;
                
            }
            else
            {
                _interactive = null;
            }
        }
    }
}












