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
        private List<IUIElement> _children = new();
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
            return _children;
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
            IUIElement element = _controller.BuildActiveElement(_levelPanel, null, this);
            ISceneConnected connectedParent = _parent.GetSceneConnection();
            if (connectedParent == null)
            {
                Debug.LogAssertion("Missing Connected Parent");
                return;
            }
            ISceneConnected connectedElement = element.GetSceneConnection();
            if (connectedElement == null)
            {
                Debug.LogAssertion("Missing Connected Element");
                return;
            }
            SceneData sceneData = connectedParent.GetAssignedScene();
            if (sceneData == null)
            {
                Debug.LogAssertion("Missing SceneData");
                return;
            }

            connectedElement.AssignScene(sceneData);
            _children.Add(element);
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
            Debug.LogWarning($"Current Parent: {_parent}");
            if (_parent == null)
            {
                Debug.LogAssertion("Missing Parent");
                return;
            }
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void Terminate()
        {
            _controller.DestroyElementAsParent(this);
        }
    }

}
