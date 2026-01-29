namespace Game.Core.Interactable
{
    using Game.Core.Player;
    using UnityEngine;

    public class DoorOneWay : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public Item GetItem()
        {
            return null;
        }

        public Rigidbody GetRigidbody()
        {
            return null;
        }

        public bool GetValuable()
        {
            return false;
        }

        public void Interact(Hands hands)
        {
            _animator.SetTrigger("Open");
        }

        public bool IsInteractable()
        {
            return true;
        }

        public bool isTool()
        {
            return false;
        }

        public void SetValuable(bool flag)
        {
            return;
        }
    }

}
