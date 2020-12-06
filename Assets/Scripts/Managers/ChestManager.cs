using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class ChestManager : MonoBehaviour
    {
        public string[] insideChest;

        [SerializeField] private Transform chestHinge;
        [SerializeField] private Transform chestLock;
        [SerializeField] private float targetScale = 1.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float chestScaleDuration = 1.0f;
        [SerializeField] private float openAngle = 70.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float openDuration = 1.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float chestLockScaleDuration = 0.5f;

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(targetScale, chestScaleDuration).SetEase(Ease.Linear);
        }

        public void OpenChest()
        {
            DOTween.Sequence()
                .Append(chestLock.DOScale(0.0f, chestLockScaleDuration))
                .Append(chestHinge.DOLocalRotate(new Vector3(0, 0, -openAngle), openDuration).SetEase(Ease.Linear));
        }

        public void CloseChest()
        {
            chestHinge.DOLocalRotate(new Vector3(0, 0, openAngle), openDuration).SetEase(Ease.Linear);
        }
    }
}