using Game.Core.Interactable;
using Game.Core.Player;
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

    public void SetValuable(bool flag)
    {
        return;
    }

    public bool GetValuable()
    {
        return false;
    }

    public void Interact(Hands hands)
    {
        Debug.Log("WIP");
    }

    public Rigidbody GetRigidbody()
    {
        return null;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public IDoor GetDoorComponent()
    {
        return null;
    }
}
