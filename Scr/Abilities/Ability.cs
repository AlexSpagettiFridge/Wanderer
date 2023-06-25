using System;
using Wanderer.Entities;
using Wanderer.Items;

namespace Wanderer.Abilities
{
    internal class Ability
    {
        private AbilityHandler handler;
        public AbilityHandler GetHandler() => handler;
        private Item sourceItem;
        public bool TryInvoke(Hero hero)
        {
            AbilityCost cost = handler.GetCost(this);
            if (!cost.IsPayable) { return false; }
            handler.Invoke(this, hero);
            cost.PayCost();
            return true;
        }
        #region cooldown
        private bool isCooldownLocked = false;
        public event Action<float> CooldownSet;
        public event Action CooldownOver;

        public void SetCooldown(float cooldown)
        {
            isCooldownLocked = true;
            Util.TimerService.AddTimer(cooldown).Timeout += () =>
            {
                isCooldownLocked = false;
                CooldownOver?.Invoke();
            };
            CooldownSet?.Invoke(cooldown);
        }

        public bool IsCoolingDown => isCooldownLocked;
        #endregion

    }
}