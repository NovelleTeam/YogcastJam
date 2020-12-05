namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
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