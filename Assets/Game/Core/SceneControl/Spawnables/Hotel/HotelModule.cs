namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        private Task<bool> IsOverlapping(IHotelRoomModule room)
        {
            Collider c = room.GetBoundaries();
            if (c == null)
                return Task.FromResult(false);

            int mask = LayerMask.GetMask("RoomBounds");

            Collider[] hits = Physics.OverlapBox(c.bounds.center, c.bounds.extents, c.transform.rotation, mask, QueryTriggerInteraction.Collide);

            DevLog.Log("Checking overlap for: " + room.GetRoomPrefab().name + " | hits: " + hits.Length, this);
            if (hits.Length > 1)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);     
        }

        private async Task<bool> TryMainRooms(HotelDoor door)
        {
            List<int> usedIndexes = new List<int>();
            foreach (IHotelRoomModule room in _possibleRooms)
            {
                DevLog.Log("Possible Room: " + room.GetRoomPrefab().name, this);
            }
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
                IHotelRoomModule room = await HotelModuleFactory.BuildHotelRoomModule(roomModule, door.GetAnchor().GetTransform());
                if (room == null)
                {
                    DevLog.LogAssertion("Failed to build room module: " + roomModule.GetRoomPrefab().name, this);
                    continue;
                }
                await ConnectModule(room, door.GetAnchor().GetTransform());
                Physics.SyncTransforms();
                bool result = await IsOverlapping(room);
                DevLog.Log("Overlap result for room: " + room.GetRoomPrefab().name + " is: " + result, this);
                if (result)
                {
                    DevLog.LogWarning("Room overlaps with existing colliders, trying another room...", this);
                    Collider c = room.GetBoundaries();
                    if (c != null)
                        c.enabled = false;

                    Destroy(room.GetRoomPrefab());
                    continue;
                }
                DevLog.Log("Successfully installed room: " + room.GetRoomPrefab().name + " for door: " + door.gameObject.name, this);
                door.SetRoomInstalled(true);
                return true;
            }
            DevLog.LogWarning("No main rooms could be fitted for door: " + door.gameObject.name, this);
            return false;
        }

        private async Task<bool> TryUtilityRooms(HotelDoor door)
        {
            List<int> usedUtilityIndexes = new List<int>();

            foreach (IHotelRoomModule room in _utilityRooms)
            {
                DevLog.Log("Utility Room: " + room.GetRoomPrefab().name, this);
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

                IHotelRoomModule room = await HotelModuleFactory.BuildHotelRoomModule(roomModule, door.GetAnchor().GetTransform());

                if (room == null)
                {
                    DevLog.LogWarning("Utility Room overlaps with existing colliders, trying another utility room...", this);
                    continue;
                }

                await ConnectModule(room, door.GetAnchor().GetTransform());

                bool result = await IsOverlapping(room);
                DevLog.Log("Overlap result for utility room: " + room.GetRoomPrefab().name + " is: " + result, this);

                if (result)
                {
                    DevLog.LogWarning("Utility Room overlaps with existing colliders, trying another utility room...", this);
                    Collider c = room.GetBoundaries();
                    if (c != null)
                        c.enabled = false;

                    Destroy(room.GetRoomPrefab());
                    continue;
                }
                DevLog.Log("Successfully installed utility room: " + room.GetRoomPrefab().name + " for door: " + door.gameObject.name, this);
                door.SetRoomInstalled(true);
                return true;

            }
            DevLog.LogWarning("No utility rooms could be fitted for door: " + door.gameObject.name, this);
            return false;
        }

        private async Task FitRoom(HotelDoor door)
        {
            bool mainResult = await TryMainRooms(door);
            DevLog.Log(mainResult.ToString(), this);
            if (!mainResult)
            {
                bool utilityResult = await TryUtilityRooms(door);
                DevLog.Log(utilityResult.ToString(), this);
                if (!utilityResult)
                {
                    DevLog.LogWarning("No rooms could be fitted for door: " + door.gameObject.name, this);
                    door.RequestDoorLock(true);
                    door.SetRoomInstalled(true);
                    return;
                }
            }
              
        }

        public async void TickCallBack(HotelDoor door)
        {
            if (!_doors.Contains(door))
                return;

            if (door.IsRoomInstalled())
                return;

            if (_possibleRooms.Count == 0 && _utilityRooms.Count == 0)
            {
                DevLog.LogAssertion("No possible rooms to install for door: " + door.gameObject.name, this);
                door.RequestDoorLock(true);
                return;
            }
            await FitRoom(door);

        }

        private Task ConnectModule(IHotelRoomModule roomModule, Transform moduleAnchor)
        {
            if (roomModule == null && moduleAnchor == null)
            {
                DevLog.LogAssertion("ConnectModule failed: roomModule or moduleAnchor is null", this);
                return Task.CompletedTask;
            }

            Transform roomRoot = roomModule.GetTransform();
            Transform roomAnchor = roomModule.GetAnchor().GetTransform();

            Vector3 projectedForward = Vector3.ProjectOnPlane(roomAnchor.forward, Vector3.up).normalized;
            Vector3 targetForward = Vector3.ProjectOnPlane(-moduleAnchor.forward, Vector3.up).normalized;

            Quaternion rot = Quaternion.LookRotation(targetForward, Vector3.up);
            roomRoot.rotation = rot;

            Vector3 offset = moduleAnchor.position - roomAnchor.position;
            roomRoot.position += offset;
            return Task.CompletedTask;

        }

        public Task InitializeModule(GameModeController controller)
        {
            _controller = controller;
            if (_doors.Count == 0)
            {
                DevLog.LogAssertion("Doors Missing....", this);
                return Task.CompletedTask;
            }
            foreach (HotelDoor door in _doors)
            {
                DevLog.Log("Setting parent module for door: " + door.gameObject.name, this);
                door.SetParentModule(this);
            }
            _controller.GetHotelRoomModules(_possibleRooms);
            _controller.GetUtilityRooms(_utilityRooms);
            if (_utilityRooms.Count == 0)
            {
                DevLog.LogWarning("No utility rooms found for module: " + _moduleName, this);
            }
            return Task.CompletedTask;
        }

        public string GetModuleName()
        {
            return _moduleName;
        }

    }

}

