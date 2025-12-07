namespace Game.Core.UI
{
    using Game.Core.DI;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem.XR;

    public class MapSelectionScreen : MonoBehaviour, IUIElement
    {
        private List<IUIElement> _childElements = new();
        private UIController _controller;
        private GameObject _instance;
        public void Activate()
        {
            CollectChildElements();
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

        public void DisplayMapSelection()
        {

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
