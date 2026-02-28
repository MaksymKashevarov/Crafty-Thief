namespace Game.Core.SceneControl.Spawnables.Hotel
{
    using UnityEngine;

    public class HotelRoomModule : MonoBehaviour, IHotelRoomModule
    {
        [SerializeField] private HotelRoomModuleAnchor _anchor;
        [SerializeField] private Collider _boundaries;

        public GameObject GetRoomPrefab()
        {
            return gameObject;
        }

        public Collider GetBoundaries()
        {
            return _boundaries;
        }


        public HotelRoomModuleAnchor GetAnchor()
        {
            return _anchor;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }

}
