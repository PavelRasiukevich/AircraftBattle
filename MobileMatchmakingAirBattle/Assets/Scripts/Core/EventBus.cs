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
        private static Dictionary<Type, List<ISubscriber>> subscribers = new Dictionary<Type, List<ISubscriber>>();

        public static void Subscribe<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetSubscriberInterfaces(subscriber.GetType()))
            {
                if (!subscribers.ContainsKey(t))
                    subscribers[t] = new List<ISubscriber>();
                subscribers[t].Add(subscriber);
            }
        }

        public static void Unsubscribe<TSubscriber>(TSubscriber subscriber) where TSubscriber : class, ISubscriber
        {
            foreach (Type t in GetSubscriberInterfaces(subscriber.GetType()))
                if (subscribers.ContainsKey(t))
                    subscribers[t].Remove(subscriber);
        }

        public static void InvokeEvent<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, ISubscriber
        {
            foreach (ISubscriber subscriber in subscribers[typeof(TSubscriber)])
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

        private static List<Type> GetSubscriberInterfaces(Type subscriberType)
        {
            List<Type> subscriberTypes = subscriberType
                .GetInterfaces()
                .Where(it =>
                    it.GetInterfaces().Contains(typeof(ISubscriber)) &&
                    it != typeof(ISubscriber))
                .ToList();
            return subscriberTypes;
        }

        private static void LogEventBus()
        {
            foreach (KeyValuePair<Type, List<ISubscriber>> pair in subscribers)
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