using Game.Core.Interactable;
using Game.Core.UI;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.DI
{
    public static class Registry
    {
        public static readonly List<IInteractable> itemRegistry = new();
        public static readonly MenuRegisrtry menuRegisrtry = new MenuRegisrtry();

        public static List<IInteractable> GetItemList()
        {
            return itemRegistry;
        }


        public static void Clear()
        {
            itemRegistry.Clear();             
        }

    }

}
