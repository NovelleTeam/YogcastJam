using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlatformManager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private Transform _platformBegin;
    [SerializeField] private Transform _platformEnd;
    [SerializeField] private Transform _generatedPathContainer;
    [SerializeField] private int index;

    private bool _wasSteppedOn;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !_wasSteppedOn)
        {
            other.gameObject.GetComponent<PlayerManager>().SetPlayerRespawn(_playerSpawn);
            other.gameObject.GetComponent<PlayerManager>().SetCurrentMainPlatformIndex(index);
            _wasSteppedOn = true;
            foreach (Transform platform in _generatedPathContainer) Destroy(platform.gameObject);
        }
    }

    public Vector3 GetPlatformBegin()
    {
        return _platformBegin.position;
    }

    public Vector3 GetPlatformEnd()
    {
        return _platformEnd.position;
    }
}