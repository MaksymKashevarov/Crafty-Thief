using System.Collections.Generic;
using Game.Core.SceneControl;

namespace Game.Core.ServiceLocating
{

    public class SpawnZoneRegistry
    {
        private List<PlacementZone> _zones = new List<PlacementZone>();
        public void Register(PlacementZone zone)
        {
            if (!_zones.Contains(zone))
            {
                _zones.Add(zone);
                DevLog.Log($"Registered spawn zone of size {zone.GetZoneSize()}.", this);
                return;
            }
            DevLog.LogWarning("Attempted to register a spawn zone that is already registered.", this);
        }
        public List<PlacementZone> GetZones(List<PlacementZone> zones)
        {
            if (_zones.Count == 0)
            {
                DevLog.LogWarning("No spawn zones have been registered.", this);
                return null;
            }
            DevLog.Log($"Returning list of {_zones.Count} spawn zones.", this);
            if (zones == null)
            {
                DevLog.LogAssetion("Provided list is null. Returning internal list.", this);
                return null;
            }
            foreach (PlacementZone zone in _zones)
            {
                zones.Add(zone);
            }
            return zones;
        }
        public void TerminateRegistry()
        {
            DevLog.Log("Terminating spawn zone registry. Clearing registered zones.", this);
            _zones.Clear();
        }
    }

}

