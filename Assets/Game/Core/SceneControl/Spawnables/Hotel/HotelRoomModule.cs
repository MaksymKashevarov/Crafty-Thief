namespace Game.Core.SceneControl.Spawnables.Hotel
{
    using UnityEngine;

    public class HotelRoomModule : MonoBehaviour, IHotelRoomModule
    {
        [SerializeField] private HotelRoomModuleAnchor _anchor;

        public GameObject GetRoomPrefab()
        {
            return gameObject;
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
