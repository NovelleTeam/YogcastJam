using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveChest : interactiveObject
{
	protected override void Awake()
	{
		IsTakeAble = false;
		DisableAfterTake = false;
	}

	public override void Interact()
	{
		
	}
}
