namespace Game.Core.Interactable
{
    using UnityEngine;

    public class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item _item;
        [SerializeField] private Storage _storage;
        [SerializeField] private StorageCore _storageCore;
        [SerializeField] private bool _isInteractable;

        public void Start()
        {
            if (_storage != null) 
            {
                _storageCore = new StorageCore(_storage);
            }           
        }

        public Item GetItem()
        {
            return _item;
        }

        public Storage GetStorage()
        {
            return _storage;
        }

        public StorageCore GetStorageCore()
        {
            return _storageCore;
        }

        public bool IsInteractable()
        {
            if (_isInteractable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }

}

