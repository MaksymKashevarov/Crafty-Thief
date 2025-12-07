using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Game.Core.DI
{
    public static class Container
    {
        private static readonly Dictionary<Type, object> _services = new();
        public static readonly CElements cElements = new CElements();

        public static void Register<T>(T instance)
        {
            _services[typeof(T)] = instance;
        }

        public static T Resolve<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {              
                return (T)service;
            }           
            return default;
        }

        public static void Clear()
        {
            _services.Clear();
        }

    }

}