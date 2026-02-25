namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using Game.Core.Factory;
    using Game.Core.Interactable;
    using Game.Core.ServiceLocating;
    using UnityEngine;

    public class HotelModule : MonoBehaviour, IModule
    {
        [SerializeField] private List<ModuleAnchor> _anchors = new List<ModuleAnchor>();
        [SerializeField] private List<HotelDoor> _doors = new List<HotelDoor>();
        private List<IHotelRoomModule> _possibleRooms = new List<IHotelRoomModule>();
        private string _moduleName;
        private GameModeController _controller;

        public List<ModuleAnchor> GetAnchors()
        {
            return _anchors;
        }

        public void Awake()
        {
            Registry.hotelRegistry.Register(this);
            _moduleName = gameObject.name;
        }

        public void TickCallBack(HotelDoor door)
        {
            if (_doors.Contains(door))
            {
                if (door.IsRoomInstalled())
                {
                    return;
                }
                _controller.GetHotelRoomModules(_possibleRooms);
                if (_possibleRooms.Count == 0)
                {
                    DevLog.LogAssertion("No possible rooms to install for door: " + door.gameObject.name, this);
                    return;
                }
                int randomIndex = Random.Range(0, _possibleRooms.Count);
                IHotelRoomModule selectedRoom = _possibleRooms[randomIndex];
                IHotelRoomModule currentRoom = HotelModuleFactory.BuildHotelRoomModule(selectedRoom, door.GetAnchor().GetTransform());

            }
        }

        public void InitializeModule(GameModeController controller)
        {
            _controller = controller;
            if (_doors.Count == 0)
            {
                DevLog.LogAssertion("Doors Missing....", this);
                return;
            }
            foreach (HotelDoor door in _doors)
            {
                DevLog.Log("Setting parent module for door: " + door.gameObject.name, this);
                door.SetParentModule(this);
            }
        }

        public string GetModuleName()
        {
            return _moduleName;
        }

    }

}

