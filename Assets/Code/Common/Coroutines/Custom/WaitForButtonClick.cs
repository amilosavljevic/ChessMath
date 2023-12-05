using UnityEngine.UI;

namespace Solitaire.Common.Coroutines
{
    public class WaitForButtonClick : Coroutine
    {
        private readonly Button button;

        public WaitForButtonClick(Button button)
        {
            this.button = button;
        }

        protected override void OnStart()
        {
            base.OnStart();
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Stop();
        }
        
        protected override void OnStop()
        {
            base.OnStop();
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}