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
        private Anchor _anchor;
        
        public Hands(Transform pivot, PlayerCore player, Anchor anchor) 
        { 
            this._pivot = pivot;
            this._player = player;
            this._anchor = anchor;
        }

        public Anchor GetAnchor()
        {
            if (_anchor == null)
            {
                Debug.LogWarning("Anchor Missing");
            }
            return _anchor;
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

