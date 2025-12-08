namespace Game.Core.ServiceLocating
{

    using Game.Core.UI;
    using UnityEngine;

    public class TContainer
    {
        private static IUIElement _tempElement;

        public void RegisterAsTemporary(IUIElement element)
        {
            if (_tempElement != null)
            {
                Debug.LogAssertion($"Container already holds: {_tempElement}");
                _tempElement = null;
                return;
            }
            _tempElement = element;
            Debug.Log($"Element as temporary set: {_tempElement}");
        }

        public IUIElement Resolve()
        {
            if (_tempElement == null)
            {
                Debug.LogAssertion("Container is empty!");
                return null;
            }
            return _tempElement;
        }
    }

}
