using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Interactive;
using DG.Tweening;
using Managers;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerInteract : MonoBehaviour
{
    public float takeDistance = 3;
    public Transform interactiveObjectDestination;
    public float interactiveObjectTravelDuration = 1;
    
    private PlayerInput _playerInput;
    private Camera _camera;
    private InteractiveObject _interactive;
    private PlayerManager _playerManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        _playerInput = new PlayerInput();
        _playerInput.Player.Interact.performed += ctx => CheckIfCanTake();
        _playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        
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
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, takeDistance))
        {
            if (hitInfo.collider != null && hitInfo.collider.gameObject.GetComponent<InteractiveObject>() != null)
            {
                _interactive = hitInfo.collider.gameObject.GetComponent<InteractiveObject>();
            }
            else
            {
                _interactive = null;
            }
            if (_interactive != null && Vector3.SqrMagnitude(_interactive.transform.position - transform.position) <= takeDistance)
            {
                if (_interactive.IsTakeAble && _interactive.destenation != null)
                {
                    interactiveObjectDestination = _interactive.destenation;
                    _interactive.gameObject.transform.DOMove(interactiveObjectDestination.position, interactiveObjectTravelDuration);
                    _interactive.Interact();
                    if(_interactive.isStickType)
                        StartCoroutine(waitForInteract(_interactive));
                    _interactive = null;
                }
                else
                {
                    _interactive.Interact();
                    _interactive = null;
                }
            }
        }
    }

    IEnumerator waitForInteract(InteractiveObject interactiveObj)
    {
        yield return new WaitForSeconds(interactiveObjectTravelDuration);
        interactiveObj.transform.position = interactiveObjectDestination.position;
        interactiveObj.transform.rotation = interactiveObjectDestination.rotation;
        _playerManager.bigPlatformManager.MakeSuprise();
        Destroy(interactiveObj.gameObject.GetComponent<Rigidbody>());
        Destroy(interactiveObj);
    }
}














