namespace Game.Core.SceneControl.Spawnables.Hotel 
{
    using System.Collections.Generic;
    using Game.Core.Interactable;
    using UnityEngine;

    public class Module : MonoBehaviour, IModule
    {
        [SerializeField] private List<ModuleAnchor> _anchors = new List<ModuleAnchor>();
        [SerializeField] private List<HotelDoor> _doors = new List<HotelDoor>();

        public List<ModuleAnchor> GetAnchors()
        {
            return _anchors;
        }

        public void InitializeModule()
        {
            if (_doors.Count == 0)
            {
                DevLog.LogAssetion("Doors Missing....", this);
                return;
            }
            foreach (HotelDoor door in _doors)
            {
                door.SetParentModule(this);
            }
        }

    }

}

