using System;
using System.Collections.Generic;

namespace Core
{
    public static class EventBus<T> where T : class, new()
    {
        private static List<object> _list = new List<object>();

        public static void Subscribe(T subscriber) => _list.Add(subscriber);

        public static void Unsubscribe(T subscriber)
        {
            if (_list.Contains(subscriber))
                _list.Remove(subscriber);
        }

        public static void InvokeEvent(Action<T> action)
        {
            foreach (var item in _list)
            {
                if (item.GetType().Equals(typeof(T)))
                    action.Invoke(item as T);
            }
        }
    }
}