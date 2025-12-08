using Game.Core.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ServiceLocating
{
    public class ElementRegistry
    {
        private readonly List<IUIElement> _elements = new();
        public void Register(IUIElement element)
        {
            if (_elements.Contains(element))
            {
                return;
            }
            _elements.Add(element);
            if (_elements.Contains(element))
            {
                Debug.Log($"{element} Registered succesfull!");
            }
        }

        public void Unregister(IUIElement element)
        {
            if (!_elements.Contains(element))
            {
                return;
            }
            _elements.Remove(element);
        }

        public IUIElement ResolveElement(IUIElement input)
        {
            if (input == null) 
            {
                Debug.LogAssertion("Element is Invalid!");
                return null;
            }
            foreach (IUIElement element in _elements)
            {
                if (element == input)
                {
                    return element;                    
                }

            }
            return null;
        }
    }

}
