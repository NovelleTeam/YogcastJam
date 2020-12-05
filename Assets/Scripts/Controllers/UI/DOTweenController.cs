using UnityEngine;
using DG.Tweening;

public class DOTweenController : MonoBehaviour
{
  public static void MoveToLocalXOneWayAndScale(Transform transform, float targetX, float moveDuration, Ease moveEaseType, float targetScale, float scaleDuration, Ease scaleEaseType, int loops, LoopType loopType)
  {
    Sequence sequence = DOTween.Sequence()
      .Append(transform.DOLocalMoveX(targetX, moveDuration).SetEase(moveEaseType))
      .Join(transform.DOScale(targetScale, scaleDuration).SetEase(scaleEaseType));
    sequence.SetLoops(loops, loopType);
  }

  public static void CanvasGroupAlpha(CanvasGroup canvasGroup, float targetAlpha, float fadeDuration)
  {
    canvasGroup.DOFade(targetAlpha, fadeDuration);
  }
}
