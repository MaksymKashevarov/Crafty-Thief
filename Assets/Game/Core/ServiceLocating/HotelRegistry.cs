using System.Collections.Generic;
using Game.Core.SceneControl.Spawnables.Hotel;

namespace Game.Core.ServiceLocating
{

    public class HotelRegistry
    {
        public static readonly List<IModule> _hotelModules = new List<IModule>();
        public static readonly List<IHotelRoomModule> _utilityRooms = new List<IHotelRoomModule>();

        private enum RegistryType
        {
            hotelRegistry,
            utilityRegistry
        }
        public void RegisterAsUtilityModule(IHotelRoomModule hotelRoom)
        {
            if (_utilityRooms.Contains(hotelRoom))
            {
                return;
            }
            _utilityRooms.Add(hotelRoom);
        }


        public void ResolveUtilityModules(List<IHotelRoomModule> modules)
        {
            if (_utilityRooms.Count == 0)
            {
                return;
            }
            foreach (var hotelRoom in _utilityRooms)
            {
                modules.Add(hotelRoom);
            }
        }

        public void Register(IModule hotel)
        {
            _hotelModules.Add(hotel);
        }
         
        public void Resolve(List<IModule> modules)
        {
            if (_hotelModules.Count == 0)
            {
                return;
            }
            foreach (var hotel in _hotelModules)
            {
                modules.Add(hotel);
            }
            TerminateRegistry(RegistryType.hotelRegistry);
        }

        private void TerminateRegistry(RegistryType type)
        {
            switch(type)
            {
                case RegistryType.hotelRegistry:
                    _hotelModules.Clear();
                    break;
                case RegistryType.utilityRegistry:
                    _utilityRooms.Clear();
                    break;
            }
        }
    }

}
