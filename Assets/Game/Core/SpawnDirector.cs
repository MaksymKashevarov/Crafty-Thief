using System.Collections.Generic;
using Game.Core.SceneControl;
using Game.Core.ServiceLocating;
using UnityEngine;

public class SpawnDirector : MonoBehaviour
{
    [SerializeField] private List<PlacementZone> _spawnZones = new List<PlacementZone>();
    [SerializeField] private SpawnableCollection _spawnableCollection;
    public void CollectSpawnZone()
    {
        _spawnZones.Clear();
        _spawnZones = Registry.spawnZoneRegistry.GetZones();
        Registry.spawnZoneRegistry.TerminateRegistry();

    }

    public void SetCollection(SpawnableCollection collection)
    {
        _spawnableCollection = collection;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
