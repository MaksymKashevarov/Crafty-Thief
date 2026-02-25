namespace Game.Core.Factory
{
    using Game.Core.SceneControl.Spawnables.Hotel;
    using UnityEngine;

    public static class HotelModuleFactory
    {
        public static IHotelRoomModule BuildHotelRoomModule(IHotelRoomModule roomModule, Transform parent)
        {
            if (roomModule == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            if (parent == null)
            {
                Debug.LogAssertion("Missing Transform");
                return null;
            }
            GameObject elementObj = roomModule.GetRoomPrefab();
            GameObject currentObject = Object.Instantiate(elementObj, parent);
            IHotelRoomModule hotelRoomModule = currentObject.GetComponent<IHotelRoomModule>();
            if (hotelRoomModule == null)
            {
                Debug.LogAssertion("Missing Component");
                return null;
            }
            Debug.Log($"Hotel Room Module Created: [{hotelRoomModule}]");
            return hotelRoomModule;
        }

    }

}

