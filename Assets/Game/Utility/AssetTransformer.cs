using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using Game.Core.SceneControl.Spawnables.Hotel;
using Game.Core.ServiceLocating;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum AssetType
{
    spawnable,
    hotel_module,
    hotel_room,
    hotel_room_utility,
}

namespace Game.Utility
{
    public static class AssetTransformer
    {
        private static readonly Dictionary<AssetType, AsyncOperationHandle<IList<GameObject>>> _handles = new();

        //public static async Task<List<IModule>> ConvertModulesAsync(AssetType type)
        //{
        //    List<IModule> result = new List<IModule>();
        //    AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(type.ToString(), null);

        //    await handle.Task;

        //    IList<GameObject> prefabs = handle.Result;
        //    if (prefabs == null)
        //    {
        //        return result;
        //    }
        //    foreach (GameObject prefab in prefabs)
        //    {
        //        if (prefab == null)
        //        {
        //            continue;
        //        }
        //        IModule module = prefab.GetComponent<IModule>();
        //        if (module == null)
        //        {
        //            continue;
        //        }
        //        result.Add(module);
        //    }
        //    return result;
        //}

        private static async Task GetUtilityRooms(AssetType type) 
        { 
            if (!_handles.TryGetValue(type, out var handle) || !handle.IsValid())
            {
                handle = Addressables.LoadAssetsAsync<GameObject>(type.ToString(), null);
                _handles[type] = handle;
            }
            if (!handle.IsDone)
                await handle.Task;

            var result = new List<IHotelRoomModule>();
            var prefabs = handle.Result;

            for (int i = 0; i < prefabs.Count; i++)
            {
                var prefab = prefabs[i];
                if (prefab == null) continue;

                var room = prefab.GetComponent<IHotelRoomModule>();
                if (room == null) continue;

                Registry.hotelRegistry.RegisterAsUtilityModule(room);
            }

        }

        public static async Task<List<IHotelRoomModule>> ConvertHotelRoomsAsync(AssetType type)
        {
            if (!_handles.TryGetValue(type, out var handle) || !handle.IsValid())
            {
                handle = Addressables.LoadAssetsAsync<GameObject>(type.ToString(), null);
                _handles[type] = handle;
            }

            if (!handle.IsDone)
                await handle.Task;

            var result = new List<IHotelRoomModule>();
            var prefabs = handle.Result;

            if (prefabs == null)
                return result;

            for (int i = 0; i < prefabs.Count; i++)
            {
                var prefab = prefabs[i];
                if (prefab == null) continue;

                var room = prefab.GetComponent<IHotelRoomModule>();
                if (room == null) continue;

                result.Add(room);
            }

            await GetUtilityRooms(AssetType.hotel_room_utility);


            return result;
        }


        public static void Release(AssetType type)
        {
            if (_handles.TryGetValue(type, out var handle) && handle.IsValid())
            {
                Addressables.Release(handle);
            }
            _handles.Remove(type);
        }

        public static async Task<List<ISpawnable>> ConvertSpawnablesAsync(AssetType type)
        {
            List<ISpawnable> result = new List<ISpawnable>();

            AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(type.ToString(), null);

            await handle.Task;

            IList<GameObject> prefabs = handle.Result;
            if (prefabs == null)
            {
                return result;
            }

            for (int i = 0; i < prefabs.Count; i++)
            {
                GameObject prefab = prefabs[i];
                if (prefab == null)
                {
                    continue;
                }

                ISpawnable spawnable = prefab.GetComponent<ISpawnable>();
                if (spawnable == null)
                {
                    continue;
                }

                result.Add(spawnable);
            }

            return result;
        }
    }
}
