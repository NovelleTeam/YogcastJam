using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vitals))]
public class interactiveEnemy : interactiveObject
{
    protected override void Awake()
    {
        IsTakeAble = false;
        DisableAfterTake = false;
    }

    public override void Interact()
    {
        GetComponent<Vitals>().TakeDamage(2);
    }
}
