using System;
using System.Collections.Generic;

namespace HospitalAutomation.Infrastructure
{
    public class SimpleContainer
    {
        private static SimpleContainer _instance;
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static SimpleContainer Instance => _instance ?? (_instance = new SimpleContainer());

        public void RegisterSingleton<T>(T service)
        {
            _services[typeof(T)] = service;
        }

        public T Resolve<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T).Name} not registered");
        }

        public T GetService<T>()
        {
            return Resolve<T>();
        }
    }
}