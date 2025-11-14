using Game.Core.Interactable;
using UnityEngine;

public class BobbyPin : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item;
    [SerializeField] private bool _isInteractable;
    [SerializeField] private bool _isTool;

    public Item GetItem()
    {
        return _item;
    }

    public void Take()
    {

    }

    public bool IsInteractable()
    {
        if (_isInteractable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isTool()
    {
        if (!_isTool)
        {
            return false;
        }
        return true;
    }
}
