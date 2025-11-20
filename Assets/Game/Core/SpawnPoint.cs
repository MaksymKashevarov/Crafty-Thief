namespace Game.Core
{
    using Game.Core.DI;
    using UnityEngine;

    public class SpawnPoint : MonoBehaviour
    {
        private void Awake()
        {
            Container.Register(this);
        }
        
        public Transform GetTransform()
        {
            return transform;
        }
    }

}

