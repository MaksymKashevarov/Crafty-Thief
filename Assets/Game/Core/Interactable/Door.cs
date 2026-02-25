namespace Game.Core.Interactable
{
    using Game.Core.Player;
    using Game.Core.SceneControl.Spawnables.Hotel;
    using Game.Core.ServiceLocating;
    using UnityEngine;

    public class Door : MonoBehaviour, IInteractable, IDoor
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _isInversed;
        private bool _isOpen = false;
        private IGameModeExtension _gameModeExtension;

        public void InstallExtension(IGameModeExtension extension)
        {
            _gameModeExtension = extension;
        }

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
            if (_isInversed)
            {
                _animator.SetTrigger("Open_Inversed");
                _isOpen = true;
                _gameModeExtension.Tick();
                return;
            }
            _animator.SetTrigger("Open");
            _isOpen = true;
            _gameModeExtension.Tick();
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
