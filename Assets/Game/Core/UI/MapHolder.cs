namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using UnityEngine;

    public class MapHolder : MonoBehaviour, IUIElement
    {
        [SerializeField] private UIController _controller;
        [SerializeField] private List<SceneData> _sceneData = new();
        private IUIElement _parent;
        [SerializeField] private SceneContainer _container;
        [SerializeField] private List<IButton> _buttons = new();
        [SerializeField] private IButton _loadButton;
        public void Activate()
        {
            Debug.Log("Working");
        }

        private void Awake()
        {
            Registry.mSRegistry.Register(this);
            Container.Register(this);
        }

        public void SetCurrentLoadButton(IButton button, IUIElement buttonElement)
        {
            if (button == null)
            {
                Debug.LogAssertion("Missing Button");
                return;
            }
            if (_loadButton != null)
            {
                IUIElement element = _loadButton.GetUIElement();
                if (element == null)
                {
                    Debug.LogAssertion("Missing Interface");
                    return;
                }
                element.Terminate();
                _loadButton = null;
            }
            IUIElement tmpElement = button.GetUIElement();
            IButton newButton = _controller.BuildButton(tmpElement, _parent.GetObject().transform, this);
            _loadButton = newButton;
            if (_loadButton == null)
            {
                Debug.LogAssertion("Button is invalid");
                return;
            }
            ISceneConnected connectedButton = buttonElement.GetSceneConnection();
            if (connectedButton == null)
            {
                Debug.LogAssertion("Missing Connection");
                return;
            }
            SceneData data = connectedButton.GetAssignedScene();
            _loadButton.SetText($"Enter Location: {data.GetSceneName()}");
        }

        private void BuildContainers()
        {
            if (_sceneData.Count == 0)
            {
                Debug.LogAssertion($"[{this.name}] List is empty");
                return;
            }
            if(_buttons.Count > 0)
            {
                Debug.LogWarning($"[{this.name}] Terminating existing display");
                for (int i = _buttons.Count - 1; i >= 0; i--)
                {
                    IUIElement button = _buttons[i].GetUIElement();
                    button.Terminate();
                    _buttons.RemoveAt(i);
                }
            }
            foreach (SceneData sceneData in _sceneData)
            {
                IButton button = _controller.BuildButton(_container, gameObject.transform, this);
                if (button == null)
                {
                    Debug.LogAssertion("Missing Button");
                    break;
                }
                _buttons.Add(button);
                button.SetText(sceneData.GetSceneName());
                IUIElement element = button.GetUIElement();
                if (element == null)
                {
                    Debug.LogAssertion("Missing element");
                    break;
                }
                ISceneConnected sceneButton = element.GetSceneConnection();
                if (sceneButton == null)
                {
                    Debug.LogAssertion("Missing Scene connection");
                    break;
                }
                sceneButton.AssignScene(sceneData);

            }
        }

        public void DisplaySelection(List<SceneData> sceneData)
        {
            Debug.Log("Displaying Data...");
            if (_sceneData.Count > 0)
            {
                _sceneData.Clear();
            }
            foreach(SceneData data in sceneData)
            {
                Debug.Log($"Data : {data.GetSceneName()}");
                _sceneData.Add(data);
            }
            Debug.Log("Building Containers...");
            BuildContainers();
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

        public void SetAsChild(IUIElement element)
        {
            return;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void SetParent(IUIElement element)
        {
            _parent = element;
        }

        public void Terminate()
        {
            Registry.mSRegistry.Unregister(this);
            Container.Unregister(this);
            _controller.DestroyElement(this);
        }

        public ISceneConnected GetSceneConnection()
        {
            return null;
        }
    }

}

