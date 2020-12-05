namespace Controllers.Interactive
{
    public class InteractiveChest : interactiveObject
    {
        public InteractiveChest()
        {
            IsTakeAble = false;
            DisableAfterTake = false;
        }

        public override void Interact()
        {
        }
    }
}