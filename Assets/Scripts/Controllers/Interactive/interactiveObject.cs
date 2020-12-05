using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveObject : MonoBehaviour
{
    public bool IsTakeAble { get; protected set; }
	public Vector3 destination;
	public float moveSpeed;
	protected float _moveDuration;

    public bool DisableAfterTake { get; protected set; }

	protected int numInteractions = 0;
	public bool shouldLimitNumInteractions;
	public int maxNumInteractions = 1;

    protected virtual void Awake()
    {
        IsTakeAble = true;
        DisableAfterTake = true;

		SetDestination(destination);
	}

	// Only useful for takeable interactiveObjects.
	public void SetDestination(Vector3 position)
	{
		if (IsTakeAble)
		{
			destination = position;

			_moveDuration = (transform.position - destination).magnitude / moveSpeed;
		}
	}

    public virtual void Interact()
    {
		if (shouldLimitNumInteractions && numInteractions >= maxNumInteractions)
		{
			return;
		}

		++numInteractions;

		if (IsTakeAble)
		{
			transform.DOMove(destination, _moveDuration).OnComplete(() => 
			{
				if (DisableAfterTake)
				{
					gameObject.SetActive(false);  // Should call Destroy here.
				}
			});
		}
    }
}
