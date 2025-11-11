namespace Game.Core.Interactable
{
    using Game.Core.UI;
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    public class StorageCore
    {
        private Storage _storage;
        private string _storageId;
        public StorageCore(Storage storage)
        {
            this._storage = storage;
        }

        public string GetId()
        {
            return _storageId;
        }

        public void SetId(string id)
        {
            if (id != null)
            {
                Debug.LogError("Can't set ID as it is already set");
                return;
            }
            _storageId = id;
        }

        public void OpenStorage(PlayerInterface playerInterface)
        {
            int maxSlots = _storage.GetStorageSlots();
            playerInterface.AccessStorage(maxSlots);
        }
    }

}

