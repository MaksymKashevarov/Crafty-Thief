namespace Game.Core.SceneControl.Spawnables.Hotel
{
    using Game.Core.Interactable;
    using UnityEngine;

    public class HotelDoor : MonoBehaviour, IGameModeExtension
    {
        [Header("STATE")]
        [SerializeField] private bool _isExtensionActivated = false;
        [Header("Main")]
        [SerializeField] private Door _door;
        [SerializeField] private HotelModule _parentModule;
        [SerializeField] private ModuleAnchor _anchor;
        private bool _isRoomInstalled = false;

        private void Awake()
        {
            if (_isExtensionActivated)
            {
                _door.InstallExtension(this);
            }
        }

        public void SetParentModule(HotelModule module)
        {
            _parentModule = module;
        }

        public ModuleAnchor GetAnchor()
        {
            return _anchor;
        }

        public bool IsRoomInstalled()
        {
            return _isRoomInstalled;
        }

        public void SetRoomInstalled(bool value)
        {
            _isRoomInstalled = value;
        }

        public void Tick()
        {
            if (!_isExtensionActivated)
            {
                return;
            }
            _parentModule.TickCallBack(this);
        }

    }

}