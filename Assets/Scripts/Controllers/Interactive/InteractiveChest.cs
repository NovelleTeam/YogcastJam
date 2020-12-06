using Managers;
using Managers.Generation;

namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
    {
        public InteractiveChest()
        {
            isTakeAble = false;
            isStickType = false;
        }

        public override void Interact()
        {
      transform.GetComponent<ChestManager>().OpenChest();
      //open menu for selecting item
      //close once item is selected
      //set index depending item chosen
      PlayerManager playerManager = FindObjectOfType<PlayerManager>();
      playerManager.SetNextMainPlatformIndex(2);
      MainGenerator mainGenerator = FindObjectOfType<MainGenerator>();
      mainGenerator.Generate(mainGenerator.mainPlatforms[playerManager.GetCurrentMainPlatformIndex()].GetPlatformEnd(), mainGenerator.mainPlatforms[playerManager.GetNextMainPlatformIndex()].GetPlatformBegin());
        }
    }
}