using UnityEngine;
using Game.Core.UI;
using Game.Core.Interactable;
using UnityEngine.UI;

public class PanelElement : MonoBehaviour, IUIElement
{
    private UIController _controller;
    private IUIElement _root;
    public GameObject GetActiveElement()
    {
        return null;
    }

    public void OnClicked()
    {
        return;
    }

    public void SetRootElement(IUIElement root)
    {
        _root = root;
    }

    public void OnTaking()
    {
        if (_controller != null)
        {
            _controller.RequestTaking(_root);
        }
    }
    public void OnPlacing()
    {
        if (_controller != null)
        {
            _controller.RequestPlacing(_root);
        }
    }

    public void SetActiveElement(GameObject element)
    {
        return;
    }

    public void SetController(UIController controller)
    {
        _controller = controller;
    }

    public Item getSlotItem()
    {
        return null;
    }

    public Image GetSlotImage()
    {
        return null;
    }

    public void SetSlotItem(Item item)
    {
        return;
    }
}
