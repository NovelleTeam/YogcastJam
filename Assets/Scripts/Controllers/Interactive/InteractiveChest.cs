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
        }
    }
}