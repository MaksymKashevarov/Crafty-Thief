namespace Game.Core.ServiceLocating
{

    using Game.Core.UI;
    using UnityEngine;
    using static UnityEditor.Rendering.FilterWindow;

    public class TContainer
    {
        private IUIElement _tempElement;
        private IButton _tempButton;

        public void RegisterAsTElement(IUIElement element)
        {
            _tempElement = element;
            Debug.Log($"Element as temporary set: {_tempElement}");
        }

        public void RegisterAsTButton(IButton button)
        {
            _tempButton = button;
            Debug.Log($"Element as temporary set: {_tempButton}");
        }
        public IUIElement ResolveTElement()
        {
            if (_tempElement == null)
            {
                Debug.LogAssertion("Container is empty!");
                return null;
            }
            return _tempElement;
        }
        public IButton ResolveTButton()
        {
            if (_tempButton == null)
            {
                Debug.LogAssertion("Container is empty!");
                return null;
            }
            return _tempButton;
        }

    }

}
