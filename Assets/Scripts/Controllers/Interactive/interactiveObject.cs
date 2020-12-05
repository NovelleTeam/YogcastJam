﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class interactiveObject : MonoBehaviour
{
    public bool IsTakeAble { get; protected set; }
    public bool DisableAfterTake { get; protected set; }
    public interactiveObject()
    {
        IsTakeAble = true;
        DisableAfterTake = true;
    }

    public virtual void Interact()
    {
        
    }
}
