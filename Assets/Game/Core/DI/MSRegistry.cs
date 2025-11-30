using Game.Core.UI;
using System.Collections.Generic;

namespace Game.Core.DI
{
    ///MAP SELECTION REGISTRY///
    public class MSRegistry
    {
        private readonly List<IUIElement> _msElements = new();

        public void Register(IUIElement element)
        {
            if (_msElements.Contains(element))
            {
                return;
            }
            _msElements.Add(element);
        }

        public void Unregister(IUIElement element)
        {
            if (!_msElements.Contains(element))
            {
                return;
            }
            _msElements.Remove(element);
        }

        public List<IUIElement> GetRegistryList()
        {
            return _msElements;
        }
    }

}
