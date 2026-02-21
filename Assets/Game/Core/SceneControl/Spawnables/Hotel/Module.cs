namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Module : MonoBehaviour, IModule
    {
        [SerializeField] private List<ModuleAnchor> _anchors = new List<ModuleAnchor>();

        public List<ModuleAnchor> GetAnchors()
        {
            return _anchors;
        }


    }

}

