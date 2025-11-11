namespace Game.Core.Player
{
    using Game.Core.Interactable;
    using Game.Core.UI;
    using UnityEngine;

    public class Hands
    {
        private Item _currentItem;
        private Transform _pivot;
        private PlayerCore _player;
        
        public Hands(Transform pivot, PlayerCore player) 
        { 
            this._pivot = pivot;
            this._player = player;
        }

        public void RecieveItem(Item item)
        {
            _currentItem = item;
        }

        public Item TransferItem(Item item)
        {
            item = _currentItem;
            _currentItem = null;
            return item;
        }

        public void Pickup(IInteractable item, GameObject sceneItem)
        {
            if (_currentItem != null) 
            {
                return;
            }

            _currentItem = item.GetItem();
            if (_currentItem != null)
            {
                Object.Destroy(sceneItem);
            }
            else
            {
                _currentItem = null;
                Debug.LogWarning("Item Pickup Error");
            }
        }

        public void Use(IInteractable item, Storage storage, PlayerInterface _interface)
        {
            bool isInteracting = _player.isCurrentlyInteracting();
            if (isInteracting)
            {
                return;
            }
            StorageCore itemStorage = item.GetStorageCore();
            if (itemStorage != null)
            {
                _player.SetInteraction(true);
                itemStorage.OpenStorage(_interface);
            }
        }

        public Item GetItem()
        {
            return _currentItem;
        }

        public void DropItem()
        {
            if (_currentItem != null)
            {
                GameObject spawnPrefab = _currentItem.GetPrefab();
                if (spawnPrefab == null)
                {
                    Debug.LogWarning("DropItem: prefab is null");
                    return;
                }

                Transform pivot = (Camera.main != null) ? Camera.main.transform : _pivot;
                float dropDistance = 2f;

                Vector3 pos = pivot.position + pivot.forward * dropDistance;
                Quaternion rot = Quaternion.LookRotation(pivot.forward, Vector3.up);

                Object.Instantiate(spawnPrefab, pos, rot);
                _currentItem = null;
            }
        }

    }

}

