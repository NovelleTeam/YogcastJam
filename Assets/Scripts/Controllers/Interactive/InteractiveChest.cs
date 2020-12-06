using DG.Tweening;
using Managers;
using Managers.Generation;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
    {
        private bool _generatedPath = false;
        public string[] insideChest;

        [SerializeField] private Transform chestHinge;
        [SerializeField] private Transform chestLock;
        [SerializeField] private float targetScale = 1.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float chestScaleDuration = 1.0f;
        [SerializeField] private float openAngle = 70.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float openDuration = 1.0f;
        [Range(0.05f, 10.0f)] [SerializeField] private float chestLockScaleDuration = 0.5f;

        protected override void Start()
        {
            isTakeAble = false;
            isStickType = false;
            
            transform.localScale = Vector3.zero;
            transform.DOScale(targetScale, chestScaleDuration).SetEase(Ease.Linear);
        }

        public override void Interact(GameObject player)
        {
            if (!_generatedPath)
            {
                transform.GetComponent<ChestManager>().OpenChest();
                //open menu for selecting item
                FindObjectOfType<UIManager>().ChestOpen();
                //close once item is selected
                //set index depending item chosen
                var playerManager = player.GetComponent<PlayerManager>();
                playerManager.SetNextMainPlatformIndex(2);
                var mainGenerator = FindObjectOfType<MainGenerator>();
                mainGenerator.Generate(
                    mainGenerator.mainPlatforms[playerManager.GetCurrentMainPlatformIndex()].GetPlatformEnd(),
                    mainGenerator.mainPlatforms[playerManager.GetNextMainPlatformIndex()].GetPlatformBegin());
                _generatedPath = true;
                OpenChest();

                GeneratePlatforms(player);
            }
        }

        private static void GeneratePlatforms(GameObject player)
        {
            var playerManager = player.GetComponent<PlayerManager>();
            playerManager.SetNextMainPlatformIndex(2);
            var mainGenerator = FindObjectOfType<MainGenerator>();
            mainGenerator.Generate(
                mainGenerator.mainPlatforms[playerManager.GetCurrentMainPlatformIndex()].GetPlatformEnd(),
                mainGenerator.mainPlatforms[playerManager.GetNextMainPlatformIndex()].GetPlatformBegin());
        }

        private void OpenChest()
        {
            DOTween.Sequence()
                .Append(chestLock.DOScale(0.0f, chestLockScaleDuration))
                .Append(chestHinge.DOLocalRotate(new Vector3(0, 0, -openAngle), openDuration).SetEase(Ease.Linear));
        }

        private void CloseChest()
        {
            chestHinge.DOLocalRotate(new Vector3(0, 0, openAngle), openDuration).SetEase(Ease.Linear);
        }
    }
}