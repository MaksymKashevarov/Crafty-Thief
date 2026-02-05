namespace Game.Core.Interactable
{
    using Game.Core.Player;
    using UnityEngine;

    public class DoorOneWay : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        private bool _isOpen = false;
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
            if (_isOpen)
            {
                _animator.SetTrigger("Close");
                _isOpen = false;
                return;
            }
            _animator.SetTrigger("Open");
            _isOpen = true;
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
