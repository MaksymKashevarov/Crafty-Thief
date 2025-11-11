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
        private GlobalDriver _globalDriver;
        public PlayerInterface(PlayerCore player,Hands playerHands, UIController controller, GlobalDriver driver) 
        { 
            this._player = player;
            this._playerHands = playerHands;
            this._controller = controller;
            this._globalDriver = driver;
        }

        private void CheckItem()
        {
                    
        }
        
        private void BuildStorageSlots(int maxSlots)
        {
            for (int i = 0; i < maxSlots; i++)
            {
                _currentStorage = _controller.AddStorageSlot(_currentStorage);
            }
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

        public void PlaceItemInStorage(IUIElement source, Item item, Image slotImg)
        {
            if (_playerHands != null && _playerHands.GetItem() != null)
            {
                Item newItem = _playerHands.TransferItem(item);
                source.SetSlotItem(newItem);
                _controller.SetImage(newItem.GetImage(), slotImg);
            }
            else
            {
                Debug.LogWarning("Hands are empty");
                return;
            }
        }

        public void TakeItemFromStorage(IUIElement source, Item item, Image slotImg)
        {
            if (_playerHands != null && _playerHands.GetItem() == null)
            {
                _playerHands.RecieveItem(item);
                _controller.SetDefaultImage(slotImg);
            }
            else
            {
                Debug.LogWarning("Hands are holding item, cant take!");
                return;
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

        public void AccessStorage(int maxSlots)
        {
            _controller.OpenStorageInterface();
            DrawCursor(true);
            BuildStorageSlots(maxSlots);
        }

        public void Refresh()
        {
            CheckItem();
        }
    }

}

