using UnityEngine;
using DG.Tweening;

public class ChestManager : MonoBehaviour
{
  [SerializeField]
  private Transform _chestHinge;
  [SerializeField]
  private Transform _chestLock;
  [SerializeField]
  private float _targetScale = 1.0f;
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
    transform.DOScale(_targetScale, _chestScaleDuration).SetEase(Ease.Linear);
  }

  public void OpenChest()
  {
    DOTween.Sequence()
      .Append(_chestLock.DOScale(0.0f, _chestLockScaleDuration))
      .Append(_chestHinge.DOLocalRotate(new Vector3(0, 0, -_openAngle), _openDuration).SetEase(Ease.Linear));
  }

  public void CloseChest()
  {
    _chestHinge.DOLocalRotate(new Vector3(0, 0, _openAngle), _openDuration).SetEase(Ease.Linear);
  }
}
