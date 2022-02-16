using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.EventBus;
using UnityEngine;

namespace Core
{
    public static class EventBus
    {
        private static Dictionary<Type, List<ISubscriber>> _subscribers = new Dictionary<Type, List<ISubscriber>>();

        public static void AddListener<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetInterfaces(subscriber.GetType()))
            {
                if (!_subscribers.ContainsKey(t))
                    _subscribers[t] = new List<ISubscriber>();
                _subscribers[t].Add(subscriber);
            }
        }

        public static void RemoveListener<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetInterfaces(subscriber.GetType()))
                if (_subscribers.ContainsKey(t))
                    _subscribers[t].Remove(subscriber);
        }

        public static void InvokeEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, ISubscriber
        {
            if (_subscribers.TryGetValue(typeof(TSubscriber), out var list))
                foreach (ISubscriber subscriber in list)
                {
                    try
                    {
                        if (subscriber != null)
                            action.Invoke(subscriber as TSubscriber);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
        }

        private static List<Type> GetInterfaces(Type subscriberType) =>
            subscriberType
                .GetInterfaces()
                .Where(it =>
                    it.GetInterfaces().Contains(typeof(ISubscriber)) &&
                    it != typeof(ISubscriber))
                .ToList();
    }
}