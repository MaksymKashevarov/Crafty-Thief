namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using Game.Core.Factory;
    using Game.Core.Interactable;
    using Game.Core.ServiceLocating;
    using UnityEngine;
    using UnityEngine.ProBuilder.Shapes;

    public class HotelModule : MonoBehaviour, IModule
    {
        [SerializeField] private List<ModuleAnchor> _anchors = new List<ModuleAnchor>();
        [SerializeField] private List<HotelDoor> _doors = new List<HotelDoor>();
        private List<IHotelRoomModule> _possibleRooms = new List<IHotelRoomModule>();
        private List<IHotelRoomModule> _utilityRooms = new List<IHotelRoomModule>();
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

        private bool IsOverlapping(Collider boundaries)
        {
            if (boundaries == null)
                return false;

            BoxCollider box = boundaries as BoxCollider;

            if (box == null)
                return false;

            Vector3 center = box.bounds.center;
            Vector3 halfExtents = box.bounds.extents;
            Quaternion rotation = box.transform.rotation;

            int mask = LayerMask.GetMask("RoomBounds");

            Collider[] hits = Physics.OverlapBox(center, halfExtents, rotation, mask, QueryTriggerInteraction.Collide);

            foreach (var hit in hits)
            {                
                if (hit.transform.IsChildOf(box.transform.root))
                    continue;

                return true;
            }

            return false;
        }

        private void DefineFittableRoom(HotelDoor door)
        {
            int maxThreshold = _possibleRooms.Count * 2;
            int attempts = 0;
            for (int i = 0; i < maxThreshold; i++)
            {
                if (attempts == maxThreshold)
                {
                    break;
                }

                int randomIndex = Random.Range(0, _possibleRooms.Count);
                IHotelRoomModule selectedRoom = _possibleRooms[randomIndex];
                IHotelRoomModule currentRoom = HotelModuleFactory.BuildHotelRoomModule(selectedRoom, door.GetAnchor().GetTransform());
                ConnectModule(currentRoom, door.GetAnchor().GetTransform());
                if (IsOverlapping(currentRoom.GetBoundaries()))
                {
                    Destroy(currentRoom.GetTransform().gameObject);
                }
                else
                {
                    door.SetRoomInstalled(true);
                    return;
                }

                attempts++;
            }
            if (attempts == maxThreshold)
            {
                int utilityAttempts = 0;
                int maxUtilityThreshold = _utilityRooms.Count * 2;


            }

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
                _controller.GetHotelRoomModules(_utilityRooms);
                if (_possibleRooms.Count == 0)
                {
                    DevLog.LogAssertion("No possible rooms to install for door: " + door.gameObject.name, this);
                    return;
                }
                int randomIndex = Random.Range(0, _possibleRooms.Count);
                IHotelRoomModule selectedRoom = _possibleRooms[randomIndex];
                IHotelRoomModule currentRoom = HotelModuleFactory.BuildHotelRoomModule(selectedRoom, door.GetAnchor().GetTransform());
                ConnectModule(currentRoom, door.GetAnchor().GetTransform());
                if (IsOverlapping(currentRoom.GetBoundaries()))
                {
                    Destroy(currentRoom.GetTransform().gameObject);
                    DefineFittableRoom(door);
                }
                door.SetRoomInstalled(true);
            }
        }

        private void ConnectModule(IHotelRoomModule roomModule, Transform moduleAnchor)
        {
            if (roomModule == null && moduleAnchor == null)
            {
                DevLog.LogAssertion("ConnectModule failed: roomModule or moduleAnchor is null", this);
                return;
            }

            Transform roomRoot = roomModule.GetTransform();
            Transform roomAnchor = roomModule.GetAnchor().GetTransform();

            Vector3 projectedForward = Vector3.ProjectOnPlane(roomAnchor.forward, Vector3.up).normalized;
            Vector3 targetForward = Vector3.ProjectOnPlane(-moduleAnchor.forward, Vector3.up).normalized;

            Quaternion rot = Quaternion.LookRotation(targetForward, Vector3.up);
            roomRoot.rotation = rot;

            Vector3 offset = moduleAnchor.position - roomAnchor.position;
            roomRoot.position += offset;

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

