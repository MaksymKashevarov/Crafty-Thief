namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MainButton : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIController _controller;
        [SerializeField] private MapSelectionScreen _mSScreen;
        
        private IUIElement _parent;
        private GameObject _instance;

        public void Activate()
        {
            if (_controller == null)
            {
                return;
            }
            Debug.Log($"I have recieved controller and been activated. Controller: {_controller}");
        }

        private void Awake()
        {
            Registry.menuRegisrtry.Register(this);
        }

        public void CollectChildElements()
        {
            return;
        }

        public List<IUIElement> GetChildElements()
        {
            return null;
        }

        public void SetParent(IUIElement element)
        {
            Debug.Log($"Parent set {element}");
            _parent = element;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        public void OnClick()
        {            
            Debug.Log("Check!");
            if ( _parent == null )
            {
                return;
            }
            _controller.BuildActiveElement(_mSScreen);
            _controller.DestroyElementAsParent(_parent);
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void Terminate()
        {
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
    }
}

