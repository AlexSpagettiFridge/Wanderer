using System;
using Wanderer.Items;

namespace Wanderer.Abilities
{
    internal class Ability
    {
        private Item sourceItem;

        #region cooldown
        private float cooldown = 0;
        public event EventHandler<float> CooldownSet;

        public void SetCooldown(float cooldown)
        {
            this.cooldown = cooldown;
            CooldownSet?.Invoke(this,cooldown);
        }
        #endregion

    }
}