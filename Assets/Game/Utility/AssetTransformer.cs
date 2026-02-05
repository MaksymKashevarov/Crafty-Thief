using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum SpawnableType
{
    spawnable,
}

namespace Game.Utility
{
    public static class AssetTransformer
    {
        
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
