using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Core
{
    //TODO: Проверку на удаление во время исполнения/foreach
    public static class EventBus
    {
        private static Dictionary<Type, List<ISubscriber>> _subscribers = new Dictionary<Type, List<ISubscriber>>();

        public static void Subscribe<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetInterfaces(subscriber.GetType()))
            {
                if (!_subscribers.ContainsKey(t))
                    _subscribers[t] = new List<ISubscriber>();
                _subscribers[t].Add(subscriber);
            }
        }

        public static void Unsubscribe<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetInterfaces(subscriber.GetType()))
                if (_subscribers.ContainsKey(t))
                    _subscribers[t].Remove(subscriber);
        }

        public static void InvokeEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, ISubscriber
        {
            if (_subscribers.TryGetValue(typeof(TSubscriber), out var list))
            {
                foreach (ISubscriber subscriber in list)
                {
                    try
                    {
                        action.Invoke(subscriber as TSubscriber);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }

                LogEventBus();
            }
        }

        private static List<Type> GetInterfaces(Type subscriberType) =>
            subscriberType
                .GetInterfaces()
                .Where(it =>
                    it.GetInterfaces().Contains(typeof(ISubscriber)) &&
                    it != typeof(ISubscriber))
                .ToList();

        private static void LogEventBus()
        {
            foreach (KeyValuePair<Type, List<ISubscriber>> pair in _subscribers)
            {
                Debug.Log($"{pair.Key.Name}  :");
                if (pair.Value.Count == 0)
                    Debug.Log("=== Empty");
                else
                    pair.Value.ForEach(rec => Debug.Log($"=== {rec.GetType().Name}"));
            }
        }
    }
}