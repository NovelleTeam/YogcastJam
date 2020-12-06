using DG.Tweening;
using Managers;
using Managers.Generation;
using System.Collections;
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
                OpenChest();
                //open menu for selecting item
                FindObjectOfType<UIManager>().ChestOpen();
                //close once item is selected
                //set index depending item chosen
                
                _generatedPath = true;

                GeneratePlatforms(player);
            }
        }

        private static void GeneratePlatforms(GameObject player)
        {
            var playerManager = player.GetComponent<PlayerManager>();
            playerManager.SetNextMainPlatformIndex(2);
            var mainGenerator = FindObjectOfType<MainGenerator>();
            mainGenerator.NextPlatform(mainGenerator.mainPlatforms[playerManager.GetCurrentMainPlatformIndex()].GetPlatformEnd());
            /*mainGenerator.Generate(
                mainGenerator.mainPlatforms[playerManager.GetCurrentMainPlatformIndex()].GetPlatformEnd(),
                mainGenerator.mainPlatforms[playerManager.GetNextMainPlatformIndex()].GetPlatformBegin(),
                new System.Collections.Generic.List<Platform>(),
                new MainGenerator.PlayerAttributes() { JumpCount = 1,JumpForce = 250, Speed = 2000 },
                new Vector2(20, 20),
                new Platform[] { new Platform(new Vector3(0, 0, 0), 1, 0.3f, 0) });*/

        }

        private void OpenChest()
        {
            DOTween.Sequence()
                .Append(chestLock.DOScale(0.0f, chestLockScaleDuration))
                .Append(chestHinge.DOLocalRotate(new Vector3(0, 0, -openAngle), openDuration).SetEase(Ease.Linear));
        }

        public IEnumerator CloseChest()
        {
            DOTween.Sequence()
                .Append(chestHinge.DOLocalRotate(new Vector3(0, 0, 0), openDuration).SetEase(Ease.Linear))
                .Append(transform.DOScale(0, chestScaleDuration).SetEase(Ease.Linear));
            yield return new WaitForSeconds(openDuration + chestScaleDuration);
            Destroy(gameObject);
        }
    }
}