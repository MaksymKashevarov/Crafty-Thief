namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class DifficultyDisplayButton : MonoBehaviour, IUIElement, IButton
    {
        [SerializeField] private UIController _controller;
        private IUIElement _parent;
        private DifficultyDisplay _display;
        [SerializeField] private ButtonSide _side;
        public void Activate()
        {
            return;
        }

        public void CollectChildElements()
        {
            return;
        }

        public List<IUIElement> GetChildElements()
        {
            return null;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public ISceneConnected GetSceneConnection()
        {
            return null;
        }

        public TextMeshProUGUI GetTextComponent()
        {
            return null;
        }

        public IUIElement GetUIElement()
        {
            return this;
        }

        public void OnClick()
        {

        }

        public void SetSide(ButtonSide side)
        {
            _side = side;
        }

        public void SetAsChild(IUIElement element)
        {
            return;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetList(List<SceneData> list)
        {
            return;
        }

        public void SetParent(IUIElement element)
        {
            _parent = element;
            _display = Container.Resolve<DifficultyDisplay>();
            if (_display == null)
            {
                Debug.LogWarning("Missing Display! Terminating... ");
                Terminate();
                return;
            }
        }

        public void SetText(string text)
        {
            return;
        }

        private void Awake()
        {
            Container.dDRegistry.RegisterDifficultyButton(this);
        }

        public void Terminate()
        {
            _controller.DestroyElement(this);
        }
    }

}
