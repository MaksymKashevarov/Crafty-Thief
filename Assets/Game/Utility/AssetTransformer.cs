using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using Game.Core.SceneControl.Spawnables.Hotel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum SpawnableType
{
    spawnable,
    Hotel_spawnable_rooms
}

namespace Game.Utility
{
    public static class AssetTransformer
    {

        public static async Task<List<IModule>> ConvertModulesAsync(SpawnableType type)
        {
            List<IModule> result = new List<IModule>();
            AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>(type.ToString(), null);

            await handle.Task;

            IList<GameObject> prefabs = handle.Result;
            if (prefabs == null)
            {
                return result;
            }
            foreach (GameObject prefab in prefabs)
            {
                if (prefab == null)
                {
                    continue;
                }
                IModule module = prefab.GetComponent<IModule>();
                if (module == null)
                {
                    continue;
                }
                result.Add(module);
            }
            return result;
        }
        
        public static async Task<List<ISpawnable>> ConvertSpawnablesAsync(SpawnableType type)
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
