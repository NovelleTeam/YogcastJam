using Managers;
using Managers.Generation;
using UnityEngine;

namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
    {
        private bool _generatedPath = false;

        public InteractiveChest()
        {
            isTakeAble = false;
            isStickType = false;
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
            }
        }
    }
}