namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using Game.Core.Factory;
    using Game.Core.Interactable;
    using Game.Core.ServiceLocating;
    using Game.Utility;
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

        private bool IsOverlapping(IHotelRoomModule room)
        {
            Collider c = room.GetBoundaries();
            if (c == null)
                return false;

            int mask = LayerMask.GetMask("RoomBounds");

            Collider[] hits = Physics.OverlapBox(c.bounds.center, c.bounds.extents, c.transform.rotation, mask, QueryTriggerInteraction.Collide);

            return hits.Length > 1;
        }

        private void FitRoom(HotelDoor door)
        {
            if (_possibleRooms.Count == 0)
            {
                DevLog.LogAssertion("No possible rooms to install for door: " + door.gameObject.name, this);
                return;
            }
            List<int> usedIndexes = new List<int>();

            // MAIN rooms
            for (int i = 0; i < _possibleRooms.Count; i++)
            {
                int randomIndex = Random.Range(0, _possibleRooms.Count);
                while (usedIndexes.Contains(randomIndex))
                {
                    randomIndex = Random.Range(0, _possibleRooms.Count);
                }

                usedIndexes.Add(randomIndex);

                IHotelRoomModule roomModule = _possibleRooms[randomIndex];
                if (roomModule == null)
                {
                    DevLog.LogAssertion("Missing Module", this);
                    continue;
                }

                IHotelRoomModule room = HotelModuleFactory.BuildHotelRoomModule(roomModule, door.GetAnchor().GetTransform());

                if (room == null)
                {
                    DevLog.LogAssertion("Failed to build room module: " + roomModule.GetRoomPrefab().name, this);
                    continue;
                }

                ConnectModule(room, door.GetAnchor().GetTransform());
                Physics.SyncTransforms();

                if (IsOverlapping(room))
                {
                    DevLog.LogWarning("Room overlaps with existing colliders, trying another room...", this);
                    Destroy(room.GetRoomPrefab());
                    continue;
                }
                DevLog.Log("Successfully installed room: " + room.GetRoomPrefab().name + " for door: " + door.gameObject.name, this);
                door.SetRoomInstalled(true);
                return;
            }

            if (_utilityRooms.Count == 0)
            {
                DevLog.LogAssertion("No utility rooms to install for door: " + door.gameObject.name, this);
                return;
            }

            List<int> usedUtilityIndexes = new List<int>();

            foreach(IHotelRoomModule hotelRoomModule in _utilityRooms)
            {
                DevLog.Log("Utility Room option: " + hotelRoomModule.GetRoomPrefab().name, this);
            }

            // UTILITY rooms
            for (int i = 0; i < _utilityRooms.Count; i++)
            {
                int randomIndex = Random.Range(0, _utilityRooms.Count);
                while (usedUtilityIndexes.Contains(randomIndex))
                {
                    randomIndex = Random.Range(0, _utilityRooms.Count);
                }

                usedUtilityIndexes.Add(randomIndex);

                IHotelRoomModule roomModule = _utilityRooms[randomIndex];
                if (roomModule == null)
                {
                    DevLog.LogAssertion("Missing Utility Module", this);
                    continue;
                }

                IHotelRoomModule room = HotelModuleFactory.BuildHotelRoomModule(roomModule, door.GetAnchor().GetTransform());

                if (room == null)
                {
                    DevLog.LogWarning("Utility Room overlaps with existing colliders, trying another utility room...", this);
                    continue;
                }

                ConnectModule(room, door.GetAnchor().GetTransform());
                Physics.SyncTransforms();

                if (IsOverlapping(room))
                {
                    DevLog.LogWarning("Utility Room overlaps with existing colliders, trying another utility room...", this);
                    Destroy(room.GetRoomPrefab());
                    continue;
                }
                DevLog.Log("Successfully installed utility room: " + room.GetRoomPrefab().name + " for door: " + door.gameObject.name, this);
                door.SetRoomInstalled(true);
                return;
            }

            DevLog.LogWarning("No room can be installed for door: " + door.gameObject.name, this);
            door.RequestDoorLock(true);
            door.SetRoomInstalled(true);
        }

        public void TickCallBack(HotelDoor door)
        {
            if (!_doors.Contains(door))
                return;

            if (door.IsRoomInstalled())
                return;

            _controller.GetHotelRoomModules(_possibleRooms);
            _controller.GetUtilityRooms(_utilityRooms);

            if (_possibleRooms.Count == 0 && _utilityRooms.Count == 0)
            {
                DevLog.LogAssertion("No possible rooms to install for door: " + door.gameObject.name, this);
                door.RequestDoorLock(true);
                return;
            }
            FitRoom(door);

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

