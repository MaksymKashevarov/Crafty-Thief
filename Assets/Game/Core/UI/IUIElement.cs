namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public interface IUIElement
    {
        void SetController(UIController controller);
        GameObject GetObject();
        void CollectChildElements();
        List<IUIElement> GetChildElements();
        void Activate(); //Universal Method
    }

}
