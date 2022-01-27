using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using static Interfaces.ISender;

namespace Assets.Scripts.Core
{
    public static class InteractionMediator
    {
        private static List<ISender> Senders { get; set; } = new List<ISender>();

        public static void Subscribe(HealthSender healthSender)
        {
            for (int i = Senders.Count - 1; i >= 0; i--)
            {
                Senders[i].Notify += healthSender;
            }
        }

        public static void Unsubscribe(HealthSender healthSender)
        {
            for (int i = Senders.Count - 1; i >= 0; i--)
            {
                Senders[i].Notify -= healthSender;
            }
        }

        public static void AddSender(ISender sender)
        {
            Senders.Add(sender);
        }

        public static void RemoveSender(ISender sender)
        {
        }
    }
}