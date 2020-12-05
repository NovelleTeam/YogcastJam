namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
    {
        public InteractiveChest()
        {
            IsTakeAble = false;
            isStickType = false;
        }

        public override void Interact()
        {
        }
    }
}