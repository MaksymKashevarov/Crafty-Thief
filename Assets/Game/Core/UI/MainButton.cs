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

        public GameObject GetObject()
        {
            return gameObject;
        }

        public void OnClick()
        {
            Debug.Log("Check!");
        }

        public void SetController(UIController controller)
        {
            _controller = controller;
        }
    }
}

