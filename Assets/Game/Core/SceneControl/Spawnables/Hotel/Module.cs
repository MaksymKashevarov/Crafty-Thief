namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using Game.Core.Interactable;
    using UnityEngine;

    public class Module : MonoBehaviour, IModule
    {
        [SerializeField] private List<ModuleAnchor> _anchors = new List<ModuleAnchor>();
        [SerializeField] private List<IDoor> _doors = new List<IDoor>();

        public List<ModuleAnchor> GetAnchors()
        {
            return _anchors;
        }


    }

}

