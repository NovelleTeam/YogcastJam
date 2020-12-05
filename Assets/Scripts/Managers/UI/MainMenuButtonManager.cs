using UnityEngine;
using DG.Tweening;

public class MainMenuButtonManager : MonoBehaviour
{
  [SerializeField]
  private float _targetX = 0.0f;
  [Range(0.5f, 10.0f), SerializeField]
  private float _moveDuration = 1.0f;
  [SerializeField]
  private Ease _moveEase = Ease.Linear;
  [SerializeField]
  private float _targetScale = 0.0f;
  [Range(0.5f, 10.0f), SerializeField]
  private float _scaleDuration = 1.0f;
  [SerializeField]
  private Ease _scaleEase = Ease.Linear;

  private void Start()
  {
    DOTweenController.MoveToLocalXOneWayAndScale(transform, _targetX, _moveDuration, _moveEase, _targetScale, _scaleDuration, _scaleEase, 0, LoopType.Restart);
  }
}
