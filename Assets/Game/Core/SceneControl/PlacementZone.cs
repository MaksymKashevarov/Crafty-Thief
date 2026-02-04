namespace Game.Core.SceneControl
{
   
    using UnityEngine;
    enum ZoneSize
    {
        Small,
        Medium,
        Large
    }
    public class PlacementZone : MonoBehaviour
    {
        [SerializeField] private ZoneSize _zoneSize;

    }

}

