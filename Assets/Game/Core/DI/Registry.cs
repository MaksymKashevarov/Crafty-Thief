using Game.Core.Interactable;
using Game.Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.DI
{
    public static class Registry
    {
        public static readonly List<IInteractable> itemRegistry = new();
        public static readonly List<IUIElement> menuElements = new();

        public static List<IInteractable> GetItemList()
        {
            return itemRegistry;
        }

        public static void RegisterAsMenuElement(IUIElement input)
        {
            if (menuElements.Contains(input))
            {
                return;
            }
            menuElements.Add(input);
        }

        public static void Clear()
        {
            itemRegistry.Clear();             
        }
    }

}
