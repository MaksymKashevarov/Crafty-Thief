namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class SceneLoadButton : MonoBehaviour, IUIElement, IButton
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LevelPanel _levelPanel;
        private IUIElement _parent;
        private ISceneConnected _connectedParent;
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
            if (_controller == null)
            {
                Debug.LogAssertion("Missing Controller");
                return;
            }
            Debug.Log($"Building: {_levelPanel.name}");
            _controller.BuildActiveElement(_levelPanel, null, this);            
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
            if (_parent == null)
            {
                return;
            }
            _connectedParent = _parent.GetSceneConnection();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void Terminate()
        {
            _controller.DestroyElement(this);
        }
    }

}
