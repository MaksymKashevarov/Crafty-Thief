using System.Collections.Generic;
using Game.Core.SceneControl.Spawnables.Hotel;

namespace Game.Core.ServiceLocating
{
    public class HotelRegistry
    {
        public static readonly List<IModule> _hotels = new List<IModule>();

        public void Register(IModule hotel)
        {
            _hotels.Add(hotel);
        }

        public void Resolve(List<IModule> modules)
        {
            if (_hotels.Count == 0)
            {
                return;
            }
            foreach (var hotel in _hotels)
            {
                modules.Add(hotel);
            }
            TerminateRegistry();
        }

        public void TerminateRegistry()
        {
            _hotels.Clear();
        }
    }

}
