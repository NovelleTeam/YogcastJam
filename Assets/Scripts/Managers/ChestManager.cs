using System;
using UnityEngine;
using DG.Tweening;

public class ChestManager : MonoBehaviour
{
  public string[] insideChest;
  
  [SerializeField]
  private Transform chestHinge;
  [SerializeField]
  private Transform chestLock;
  [SerializeField]
  private float targetScale = 1.0f;
  [Range(0.05f, 10.0f), SerializeField]
  private float _chestScaleDuration = 1.0f;
  [SerializeField]
  private float _openAngle = 70.0f;
  [Range(0.05f, 10.0f), SerializeField]
  private float _openDuration = 1.0f;
  [Range(0.05f, 10.0f), SerializeField]
  private float _chestLockScaleDuration = 0.5f;

  private void Start()
  {
    transform.localScale = Vector3.zero;
    transform.DOScale(targetScale, _chestScaleDuration).SetEase(Ease.Linear);
  }

  public void OpenChest()
  {
    DOTween.Sequence()
      .Append(chestLock.DOScale(0.0f, _chestLockScaleDuration))
      .Append(chestHinge.DOLocalRotate(new Vector3(0, 0, -_openAngle), _openDuration).SetEase(Ease.Linear));
  }

  public void CloseChest()
  {
    chestHinge.DOLocalRotate(new Vector3(0, 0, _openAngle), _openDuration).SetEase(Ease.Linear);
  }
}
