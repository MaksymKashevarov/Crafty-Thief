namespace Game.Core.SceneControl.Spawnables.Hotel
{
    using UnityEngine;

    public class ModuleAnchor : MonoBehaviour
    {
        [SerializeField] private bool _isLocked = false;


        public bool IsLocked()
        {
            return _isLocked;
        }
    }

}

