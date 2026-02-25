using UnityEngine;

namespace Game.Core.SceneControl.Spawnables.Hotel
{
    public interface IHotelRoomModule
    {
        GameObject GetRoomPrefab();
        HotelRoomModuleAnchor GetAnchor();
        Transform GetTransform();
    }

}