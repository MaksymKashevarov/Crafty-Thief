namespace Game.Core.ServiceLocating
{
    using Game.Core.UI;
    using System.Collections.Generic;
    using UnityEngine;

    public class MenuRegisrtry
    {
        private readonly List<IUIElement> _menuElements = new();

        public void Register(IUIElement element)
        {
            if (_menuElements.Contains(element))
            {
                return;
            }
            _menuElements.Add(element);
        }

        public void Unregister(IUIElement element)
        {
            if (!_menuElements.Contains(element))
            {
                return;
            }
            _menuElements.Remove(element);
        }

        public List<IUIElement> GetRegistryList()
        {
            return _menuElements;
        }

    }

}

