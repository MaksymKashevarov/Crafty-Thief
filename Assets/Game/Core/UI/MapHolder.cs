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
        public void Activate()
        {
            Debug.Log("Working");
        }

        private void Awake()
        {
            Registry.mSRegistry.Register(this);
            Container.Register(this);
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
                    _buttons[i].Terminate();
                    _buttons.RemoveAt(i);
                }
            }
            foreach (SceneData sceneData in _sceneData)
            {
                Debug.LogAssertion($"[{this.name}] Building");
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
            _controller.DestroyElement(this);
        }

        public ISceneConnected GetSceneConnection()
        {
            return null;
        }
    }

}

