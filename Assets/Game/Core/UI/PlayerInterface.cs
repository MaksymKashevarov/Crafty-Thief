namespace Game.Core.UI
{
    using Game.Core.Interactable;
    using Game.Core.Player;
    using Game.Core.SceneControl;
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEditor.PackageManager.Requests;
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
        private GlobalDriver _driver;
        public PlayerInterface(PlayerCore player,Hands playerHands, UIController controller, GlobalDriver driver) 
        { 
            this._player = player;
            this._playerHands = playerHands;
            this._controller = controller;
            this._driver = driver;
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

        public void RequestMenuBuild()
        {
            _controller.DisplayMenu();
        }

        public void RequestSceneSwitch(SceneData level)
        {
            if (level == null)
            {
                DevLog.LogAssetion("Level data is null", this);
                return;
            }
            _driver.RequestSwitchScene(level);
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


        public void Refresh()
        {

        }
    }

}

