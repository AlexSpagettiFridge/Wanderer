using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Wanderer.Entities;
using Wanderer.Items;

namespace Wanderer.Abilities
{
    internal class Ability
    {
        private AbilityHandler handler;
        public string HandlerName => handler.Name;
        private Item sourceItem;
        public Item SourceItem => sourceItem;

        public Ability(AbilityHandler handler, Item sourceItem = null)
        {
            this.handler = handler;
            this.sourceItem = sourceItem;
        }
        public AbilityHandler GetHandler() => handler;
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

        #region Json
        public JsonNode GetJson() => JsonSerializer.SerializeToNode(this);
        public static Ability CreateFromJson(JsonDocument json) => CreateFromJson(json.RootElement);
        public static Ability CreateFromJson(JsonElement json)
        {
            if (!json.TryGetProperty("HandlerName", out JsonElement handlerNameElement) ||
                !json.TryGetProperty("SourceItem", out JsonElement sourceItemElement)
            ) { return null; }

            AbilityHandler abilityHandler = AbilityBank.GetByName(handlerNameElement.GetString());
            Item sourceItem = Item.CreateFromJson(sourceItemElement);

            return new Ability(abilityHandler, sourceItem);
        }
        #endregion
    }
}