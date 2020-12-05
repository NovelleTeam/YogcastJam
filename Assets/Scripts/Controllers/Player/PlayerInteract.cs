using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.Interactive;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float takeDistance = 3;
    public Transform interactiveObjectDestination;
    public float interactiveObjectTravelDuration = 1;
    
    private PlayerInput _playerInput;
    private Camera _camera;
    private InteractiveObject _interactive;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
        _playerInput = new PlayerInput();
        _playerInput.Player.Interact.performed += ctx => CheckIfCanTake();
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
                print("aa");
                if (_interactive.IsTakeAble && _interactive.destenation != null)
                {
                    print("bb");
                    interactiveObjectDestination = _interactive.destenation;
                    _interactive.gameObject.transform.DOMove(interactiveObjectDestination.position, interactiveObjectTravelDuration);
                    _interactive.Interact();
                    if(_interactive.DisableAfterTake)
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
        interactiveObj.gameObject.SetActive(false);
    }
}