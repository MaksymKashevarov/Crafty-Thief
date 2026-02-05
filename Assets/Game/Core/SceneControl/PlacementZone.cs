namespace Game.Core.SceneControl
{
    using Game.Core.ServiceLocating;
    using UnityEngine;

    public class PlacementZone : MonoBehaviour
    {
        [SerializeField] private SpawnSize _zoneSize;
        [SerializeField] private Collider _zoneCollider;

        public SpawnSize GetZoneSize()
        {
            return _zoneSize;
        }

        public Collider GetCollider()
        {
            return _zoneCollider;
        }

        private void Awake()
        {
            Registry.spawnZoneRegistry.Register(this);
        }
    }

}

