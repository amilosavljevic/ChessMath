namespace Solitaire.Common.Coroutines
{
    public class WaitForTicks : Coroutine
    {
        private int remainingTicks;

        public WaitForTicks(int remainingTicks)
        {
            this.remainingTicks = remainingTicks;
        }

        protected override void Update()
        {
            remainingTicks--;
            if (remainingTicks < 0)
                Stop();
        }
    }
}
