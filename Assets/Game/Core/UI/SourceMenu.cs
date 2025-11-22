namespace Game.Core.UI
{
    using Game.Core.DI;
    using UnityEngine;

    public class SourceMenu : MonoBehaviour, IUIElement
    {
        [SerializeField] private IUIElement _playButton;
        private UIController _controller;

        public void SetController(UIController controller)
        {
            _controller = controller;           
        }
        

        public void CollectElements()
        {

        }

        public void Activate()
        {
            CollectElements();
            return;
        }

        private void Awake()
        {
            Registry.RegisterAsMenuElement(this);
        }
    }
}

