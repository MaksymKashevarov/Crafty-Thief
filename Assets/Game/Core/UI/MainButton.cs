namespace Game.Core.UI
{
    using Game.Core.ServiceLocating;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class MainButton : MonoBehaviour, IUIElement, IButton
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UIController _controller;
        [SerializeField] private MapSelectionScreen _mSScreen;
        
        private IUIElement _parent;
        private GameObject _instance;
        private bool _isClicked = false;
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
            Container.tContainer.RegisterAsTButton(this);
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
            if ( _isClicked )
            {
                return;
            }
            Debug.Log("Check!");
            if ( _parent == null )
            {
                return;
            }
            _isClicked = true;
            _controller.BuildActiveElement(_mSScreen);
            if (_parent == null)
            {
                Debug.LogWarning("No Parent");
            }
            _controller.DestroyElementAsParent(_parent);
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }

        public void Terminate()
        {
            Registry.menuRegisrtry.Unregister(this);
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

        public void SetText(string text)
        {
            _text.text = text;
        }

        public TextMeshProUGUI GetText()
        {
            return _text;
        }
    }
}

