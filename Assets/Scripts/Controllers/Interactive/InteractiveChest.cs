namespace Controllers.Interactive
{
    public class InteractiveChest : InteractiveObject
    {
        protected override void Awake()
        {
            isTakeAble = false;
            disableAfterTake = false;
        }

        public override void Interact()
        {
        }
    }
}