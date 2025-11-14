namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using Game.Core.Player;
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerInterface
    {
        private PlayerCore _player;
        private Hands _playerHands;
        private UIController _controller;
        private List<IUIElement> _currentStorage = new();
        private int _handsInventorySize;
        private List<string> _activeStealList = new();
        public PlayerInterface(PlayerCore player,Hands playerHands, UIController controller) 
        { 
            this._player = player;
            this._playerHands = playerHands;
            this._controller = controller;
        }

        public bool IsItemInList(IInteractable item)
        {
            if (item != null)
            {
                Item currentItem = item.GetItem();
                if (currentItem != null)
                {
                    string itemName = currentItem.GetItemName();
                    if (_activeStealList.Contains(itemName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateActiveList(IInteractable item)
        {
            Item currentItem = item.GetItem();
            if (currentItem != null)
            {
                string key = currentItem.GetItemName();
                if (key != null)
                {
                    _activeStealList.Remove(key);
                    _controller.RemoveItemInList(key);
                }
            }
        }

        public List<string> GetActiveList()
        {
            if (_activeStealList != null)
            {
                return _activeStealList;
            }
            else
            {
                Debug.LogWarning("List is empty");
                return null;
            }
        }

        public bool IsInventoryFull()
        {
            List<Item> playerInventory = _playerHands.GetHandsInventory();
            if (playerInventory.Count > _handsInventorySize) 
            {
                return true;
            }
            return false;
        }

        public void RequestListBuild(List<string> stealList)
        {
            if (stealList.Count > 0)
            {
                _controller.ShowStealList(stealList, _activeStealList);
            }
        }

        public void SetInventorySize(int size)
        {
            _handsInventorySize = size;
        }


        public void DrawCursor(bool mode)
        {
            if (mode)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }


        public void TerminateInterface()
        {
            if (_player.isCurrentlyInteracting())
            {
                Debug.Log("Requesting termination of interface");
                _controller.TerminateCurrentInterface();
                _currentStorage?.Clear();
                _currentStorage = new List<IUIElement>();
                _player.SetInteraction(false);
            }
        }

        public void ClearElements(IUIElement except)
        {
            if (_currentStorage == null) return;

            foreach (IUIElement element in _currentStorage)
            {
                if (element == except)
                    continue;

                GameObject currentElement = element.GetActiveElement();
                if (currentElement != null)
                {
                    _controller.DestroyElement(currentElement);
                }
            }
        }

        public void Refresh()
        {

        }
    }

}

