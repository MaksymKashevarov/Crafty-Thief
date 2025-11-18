using Game.Core.Player;
using UnityEngine;

namespace Game.Core.Interactable
{
    public interface IInteractable
    {
        Item GetItem();
        bool IsInteractable();
        void SetValuable(bool flag);
        bool isTool();
        bool GetValuable();
        void Interact(Hands hands);
        Rigidbody GetRigidbody();
        GameObject GetGameObject();
    }

}

