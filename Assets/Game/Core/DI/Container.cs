using System;
using System.Collections.Generic;

namespace Game.Core.DI
{
    public static class Container
    {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T instance)
        {
            _services[typeof(T)] = instance;
        }

        public static T Resolve<T>()
        {
            return (T)_services[typeof(T)];
        }

    }

}