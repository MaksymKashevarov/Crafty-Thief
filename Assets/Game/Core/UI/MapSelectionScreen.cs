namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using Game.Core.SceneControl;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem.XR;

    public class MapSelectionScreen : MonoBehaviour, IUIElement
    {
        [SerializeField] private CategoryButton _categoryButton;
        [SerializeField] private Transform _categoryParent;
        private List<IUIElement> _childElements = new();
        private UIController _controller;
        private GameObject _instance;
        private List<CategoryButton> _buttons = new();
        public void Activate()
        {
            CollectChildElements();
            Debug.Log("check!");
        }

        public void CollectChildElements()
        {
            if (_childElements.Count > 0)
            {
                _childElements.Clear();
            }

            List<IUIElement> elements = Registry.mSRegistry.GetRegistryList();
            if (elements == null)
            {
                return;
            }
            if (elements.Count == 0)
            {
                return;
            }
            foreach (IUIElement element in elements)
            {
                Debug.Log($"{element} Added from Registry");
                _childElements.Add(element);
            }
        }

        private void Start()
        {
            if (_controller == null)
            {
                Debug.LogAssertion("Missing Controller In Set!");
                return;
            }
            DisplayMapSelection();
        }

        public void DisplayMapSelection()
        {
            if (_controller == null)
            {
                Debug.LogAssertion("Missing Controller");
                return;
            }
            Debug.Log("Creating base map selection...");
            SceneDatabase dataBase = _controller.GetDatabase();
            Dictionary<string, List<SceneData>> sceneData = dataBase.GetReferenceBundle();
            for (int i = 0; i < sceneData.Count; i++)
            {
                Debug.Log(i);
            }
            foreach (string key in sceneData.Keys)
            {
                if (key == null)
                {
                    Debug.LogAssertion("Missing Key!");
                    break;
                }
                _controller.BuildActiveElement(_categoryButton, _categoryParent);
            }
        }

        public List<IUIElement> GetChildElements()
        {
            return _childElements;
        }

        public GameObject GetInstance()
        {
            return _instance;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
            if (_controller == null)
            {
                Debug.LogAssertion("[SET] Missing Controller!");
                return;
            }
            Debug.Log($"Controller set: {_controller}");
            _controller.CallbackActivation(this);
        }

        public void SetInstance(GameObject instance)
        {
            _instance = instance;
        }

        public void SetParent(IUIElement element)
        {
            return;
        }

        public void Terminate()
        {
            _childElements.Clear();
            _childElements = null;
            _controller.DestroyElement(this);
        }
    }

}
