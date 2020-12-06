using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowOnSpawn : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1.0f, 1.0f).SetEase(Ease.Linear);
    }
}
