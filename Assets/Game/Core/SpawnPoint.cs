namespace Game.Core
{
    using Game.Core.ServiceLocating;
    using UnityEngine;

    public class SpawnPoint : MonoBehaviour
    {
        private void Awake()
        {
            Container.Register(this);
        }
        
        // REFACTOR
        public Transform GetTransform()
        {
            return transform;
        }
    }

}

