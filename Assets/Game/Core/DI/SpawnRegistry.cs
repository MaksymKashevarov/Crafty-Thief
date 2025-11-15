using Game.Core.Interactable;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.DI
{
    public static class SpawnRegistry
    {
        public static readonly List<IInteractable> itemRegistry = new();

        public static void Clear()
        {
            itemRegistry.Clear();             
        }
    }

}
