namespace Game.Core.Factory
{
    using Game.Core.UI;
    using UnityEngine;

    public static class UIFactory
    {
        public static IUIElement BuildElement(IUIElement element, Transform parent)
        {
            if (element == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            if (parent == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            GameObject elementObj = element.GetObject();
            GameObject currentObject = Object.Instantiate(elementObj, parent);
            IUIElement uIElement = currentObject.GetComponent<IUIElement>();
            if (uIElement == null)
            {
                Debug.LogAssertion("Missing Component");
                return null;
            }
            Debug.Log($"Element Created: [{uIElement}]");
            return uIElement;
        }

        public static IButton BuildButton(IUIElement element, Transform parent)
        {
            if (element == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            if (parent == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            GameObject elementObj = element.GetObject();
            GameObject currentObject = Object.Instantiate(elementObj, parent);
            IButton uIElement = currentObject.GetComponent<IButton>();
            if (uIElement == null)
            {
                Debug.LogAssertion("Missing Component");
                return null;
            }
            Debug.Log($"Button Created: [{uIElement}]");
            return uIElement;
        }

    }

}

