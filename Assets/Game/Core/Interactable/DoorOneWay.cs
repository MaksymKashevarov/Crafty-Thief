namespace Game.Core.Interactable
{
    using Game.Core.Player;
    using Game.Core.ServiceLocating;
    using UnityEngine;

    public class DoorOneWay : MonoBehaviour, IInteractable, IDoor
    {
        [SerializeField] private Animator _animator;
        private bool _isOpen = false;

        public IDoor GetDoorComponent()
        {
            return this;
        }

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

        public void SelfRegister()
        {
            Registry.spawnRegistry.RegisterAsDoor(this);
        }

        private void Awake()
        {
            SelfRegister();
        }

        public void SetValuable(bool flag)
        {
            return;
        }
    }

}
