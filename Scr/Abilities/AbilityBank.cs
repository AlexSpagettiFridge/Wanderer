using System;
using System.Linq;
using System.Collections.Generic;
using Wanderer.Abilities.DefaultAbilities;

namespace Wanderer.Abilities
{
    internal static class AbilityBank
    {
        private static List<AbilityHandler> handlers = new List<AbilityHandler>();

        internal static void Init()
        {
            RegisterHandler(new BasicAttack());
        }

        public static void RegisterHandler(AbilityHandler handler)
        {
            handlers.Add(handler);
        }
        public static AbilityHandler Find(Predicate<AbilityHandler> predicate) => handlers.Find(predicate);

        public static AbilityHandler GetByName(string name) => Find((x) => x.Name == name);
    }
}