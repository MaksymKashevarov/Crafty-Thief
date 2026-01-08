namespace Game.Core.UI 
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class DifficultyDisplay : MonoBehaviour, IUIElement, IButton
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private DifficultyDisplayButton _button;
        private IUIElement _parent;

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
            return _text;            
        }

        public IUIElement GetUIElement()
        {
            return this;
        }

        public void OnClick()
        {
            return;
        }

        public void ShowButtons(bool lSide = false, bool rSide = false)
        {
            if (lSide)
            {
                DifficultyDisplayButton button = SetupButton();
                if (button == null)
                {
                    Debug.LogAssertion("Missing Button");
                    return;
                }
                button.SetSide(ButtonSide.left);
            }
            if (rSide)
            {
                DifficultyDisplayButton button = SetupButton();
                if (button == null)
                {
                    Debug.LogAssertion("Missing Button");
                    return;
                }
                button.SetSide(ButtonSide.right);
            }
        }

        private DifficultyDisplayButton SetupButton()
        {
            IButton button = _controller.BuildButton(_button, this.transform, this);
            DifficultyDisplayButton currentButton = Container.dDRegistry.ResolveDifficultyButton();
            if (currentButton == null)
            {
                Debug.LogAssertion("Missing Button");
                _controller.DestroyElement(button.GetUIElement());
                return null;
            }
            return currentButton;
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
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void Terminate()
        {
            Container.Unregister(this);
            _controller.DestroyElement(this);
        }
        private void Awake()
        {
            Container.Register<DifficultyDisplay>(this);

        }
    }

}

