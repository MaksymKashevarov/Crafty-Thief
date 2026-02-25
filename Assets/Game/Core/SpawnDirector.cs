using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using Game.Core.ServiceLocating;
using Game.Utility;
using UnityEngine;

public class SpawnDirector : MonoBehaviour
{
    [SerializeField] private List<PlacementZone> _spawnZones = new List<PlacementZone>();
    private List<ISpawnable> _spawnables = new List<ISpawnable>();
    //[SerializeField] private SpawnableCollection _spawnableCollection;
    public async Task GenInitialize()
    {
        DevLog.Log("Initializing Spawn Director", this);
        _spawnZones.Clear();
        Registry.spawnRegistry.GetZones(_spawnZones);
        DevLog.Log("Spawn Zones Found: " + _spawnZones.Count, this);

        if (_spawnZones.Count == 0)
        {
            DevLog.LogWarning("No spawn zones found", this);
            return;
        }
        Registry.spawnRegistry.TerminateRegistry();
        await InitializeSpawnables();
        await GenerateSpawnables();
    }

    private async Task InitializeSpawnables()
    {
        _spawnables = await AssetTransformer.ConvertSpawnablesAsync(AssetType.spawnable);

        DevLog.Log("Spawnables Loaded: " + _spawnables.Count, this);
    }

    private Task GenerateSpawnables()
    {
        foreach (PlacementZone zone in _spawnZones)
        {
            SpawnSize size = zone.GetZoneSize();

            ISpawnable chosen = GetRandomBySize(size);
            if (chosen == null)
            {
                DevLog.LogAssertion("No spawnable found for size: " + size, this);
                break;
            }
            SpawnInZone(zone, chosen.GetGameObject());
        }
        return Task.CompletedTask;
    }

    private void SpawnInZone(PlacementZone zone, GameObject prefab)
    {
        Collider col = zone.GetCollider();
        Bounds bounds = col.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        float y = bounds.max.y;

        Vector3 position = new Vector3(x, y, z);

        Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        Instantiate(prefab, position, rotation);
    }

    private ISpawnable GetRandomBySize(SpawnSize size)
    {
        List<ISpawnable> candidates = new List<ISpawnable>();

        for (int i = 0; i < _spawnables.Count; i++)
        {
            ISpawnable spawnable = _spawnables[i];
            if (spawnable == null)
            {
                continue;
            }

            if (spawnable.GetFixedSize() != size)
            {
                continue;
            }

            candidates.Add(spawnable);
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, candidates.Count);
        return candidates[index];
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
