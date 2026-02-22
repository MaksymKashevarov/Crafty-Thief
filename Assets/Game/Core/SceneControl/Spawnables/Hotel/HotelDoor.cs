namespace Game.Core.SceneControl.Spawnables.Hotel
{
    using Game.Core.Interactable;
    using UnityEngine;

    public class HotelDoor : MonoBehaviour
    {
        [Header("STATE")]
        [SerializeField] private bool _isExtensionActivated = false;
        [Header("Main")]
        [SerializeField] private Door _door;
        [SerializeField] private Module _parentModule;

        public void SetParentModule(Module module)
        {
            _parentModule = module;
        }

    }

}