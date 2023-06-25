using System;
using System.Linq;
using System.Collections.Generic;

namespace Wanderer.Abilities
{
    internal static class AbilityBank
    {
        private static List<AbilityHandler> handlers = new List<AbilityHandler>();

        internal static void Init()
        {

        }

        public static void RegisterHandler(AbilityHandler handler)
        {
            handler.Id = handlers.Count;
            handlers.Add(handler);
        }
        public static AbilityHandler Find(Predicate<AbilityHandler> predicate) => handlers.Find(predicate);
    }
}