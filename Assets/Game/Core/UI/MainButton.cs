namespace Game.Core.UI
{
    using Game.Core.DI;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MainButton : MonoBehaviour, IUIElement
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIController _controller;
        private IUIElement _parent;

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
            Registry.RegisterAsMenuElement(this);
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
            _controller.DestroyElementAsParent(_parent);
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void Terminate()
        {            
            Destroy(gameObject);
        }
    }
}

