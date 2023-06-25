namespace Wanderer.Abilities.Costs
{
    internal class StaminaCost : AbilityCost
    {
        private int amount = 0;

        public StaminaCost(int amount)
        {
            this.amount = amount;
        }

        public override bool IsPayable => Util.GameData.HeroData.Stamina.Value >= amount;
        public override void PayCost() => Util.GameData.HeroData.Stamina.Exhaust(amount);
    }
}