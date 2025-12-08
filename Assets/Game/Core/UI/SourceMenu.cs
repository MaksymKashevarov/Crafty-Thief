namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using UnityEngine;

    public class SourceMenu : MonoBehaviour, IUIElement
    {
        private List<IUIElement> _childElements = new();
        [SerializeField] private UIController _controller;
        private GameObject _instance;


        public void SetController(UIController controller)
        {
            Debug.Log("Controller set");
            _controller = controller;
            if (_controller == null)
            {
                Debug.LogAssertion("Missing Controller");
                return;
            }
            //Debug.Log("[CALLBACK]");
            //_controller.CallbackActivation(this);
        }
        
        public GameObject GetObject()
        {
            return gameObject;
        }

        public List<IUIElement> GetChildElements()
        {
            return _childElements;
        }

        public void CollectChildElements()
        {
            if (_childElements.Count > 0)
            {
                _childElements.Clear();
            }

            List<IUIElement> elements = Registry.menuRegisrtry.GetRegistryList();
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

        public void Activate()
        {
            CollectChildElements();
            return;
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

        public void SetInstance(GameObject instance)
        {
            _instance = instance;
        }

        public GameObject GetInstance()
        {
            return _instance;
        }
        public void Awake()
        {
            Debug.Log("Registering");
            Container.tContainer.RegisterAsTemporary(this);
        }


    }
}

