namespace Game.Core.UI
{
    using Game.Core.DI;
    using System.Collections.Generic;
    using UnityEngine;

    public class SourceMenu : MonoBehaviour, IUIElement
    {
        [SerializeField] private GameObject _playButton;
        private List<IUIElement> _childElements = new();
        private UIController _controller;


        public void SetController(UIController controller)
        {
            _controller = controller;           
        }
        
        public GameObject GetObject()
        {
            return gameObject;
        }

        public void CollectChildElements()
        {
            List<IUIElement> elements = Registry.GetMenuElements();
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

            }

        }

        public void Activate()
        {
            CollectChildElements();
            return;
        }

        private void Awake()
        {
            Registry.RegisterAsMenuElement(this);
        }
    }
}

