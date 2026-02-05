namespace Game.Core.SceneControl
{
    using UnityEngine;

    public class Spawnable : MonoBehaviour, ISpawnable
    {
        [SerializeField] private SpawnSize _fixedSize;
        public SpawnSize GetFixedSize()
        {
            return _fixedSize;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}


