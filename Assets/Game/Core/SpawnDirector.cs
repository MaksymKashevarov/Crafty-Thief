using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.SceneControl;
using Game.Core.ServiceLocating;
using Game.Utility;
using UnityEngine;

public class SpawnDirector : MonoBehaviour
{
    [SerializeField] private List<PlacementZone> _spawnZones = new List<PlacementZone>();
    [SerializeField] private List<ISpawnable> _spawnables = new List<ISpawnable>();
    //[SerializeField] private SpawnableCollection _spawnableCollection;
    public async Task GenInitialize()
    {
        DevLog.Log("Initializing Spawn Director", this);
        _spawnZones.Clear();
        Registry.spawnZoneRegistry.GetZones(_spawnZones);
        DevLog.Log("Spawn Zones Found: " + _spawnZones.Count, this);

        if (_spawnZones.Count == 0)
        {
            DevLog.LogWarning("No spawn zones found", this);
            return;
        }
        Registry.spawnZoneRegistry.TerminateRegistry();
        await InitializeSpawnables();
    }

    private async Task InitializeSpawnables()
    {
        _spawnables = await AssetTransformer.ConvertSpawnablesAsync(SpawnableType.spawnable);

        DevLog.Log("Spawnables Loaded: " + _spawnables.Count, this);
    }

    public void SetCollection(SpawnableCollection collection)
    {
        //_spawnableCollection = collection;
        DevLog.LogWarning("Not Implemented", this);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
