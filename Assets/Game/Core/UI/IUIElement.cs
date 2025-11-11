using Game.Core.Interactable;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.UI
{
    public interface IUIElement
    {
        void SetController(UIController controller);
        void OnClicked();
        void SetActiveElement(GameObject element);
        Item getSlotItem();
        GameObject GetActiveElement();
        void SetRootElement(IUIElement root);
        void SetSlotItem(Item item);
        Image GetSlotImage();
    }

}
