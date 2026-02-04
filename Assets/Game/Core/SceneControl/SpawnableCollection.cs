namespace Game.Core.SceneControl
{
    using System.Collections.Generic;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;


    [CreateAssetMenu(menuName = "Config/SpawnableCollection")]
    public class SpawnableCollection : ScriptableObject
    {
        [SerializeField] private List<Spawnable> _spawnables = new List<Spawnable>();
        [SerializeField] private DefaultAsset _collectionFolder;

        public List<Spawnable> GetSpawnables()
        {
            return _spawnables;
        }
    }

}

