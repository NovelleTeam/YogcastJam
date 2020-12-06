using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDestanetionSeter : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent _agent;


    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(target != null)
            UpdatePosition();
    }
    
    private void UpdatePosition()
    {
        _agent.SetDestination(target.position);
    }
}
