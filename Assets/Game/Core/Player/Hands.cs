namespace Game.Core.Player
{
    using Game.Core.Interactable;
    using Game.Core.UI;
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine;

    public class Hands
    {
        private Item _currentItem;
        private List<Item> _itemsInHands = new();
        private Transform _pivot;
        private PlayerCore _player;
        
        public Hands(Transform pivot, PlayerCore player) 
        { 
            this._pivot = pivot;
            this._player = player;
        }

        public List<Item> GetHandsInventory()
        {
            return _itemsInHands;
        }

        public void Interact(IInteractable item)
        {
            if (item != null)
            {
                item.Interact(this);
            }
        }

    }

}

