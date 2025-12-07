using Game.Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.DI
{
    public class CElements
    {
        private static readonly Dictionary<Type, object> _elements = new();
        public void Register<T>(T instance)
        {
            Debug.Log($"Registering: {instance}");
            _elements[typeof(T)] = instance;
            Debug.Log(_elements[typeof(T)]);
        }

        public T Resolve<T>()
        {
            if (_elements.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            Debug.LogAssertion("Missing!");
            return default;
        }

        public void Clear()
        {
            _elements.Clear();
        }
    }

}
