namespace Wanderer.Abilities
{
    internal abstract class AbilityCost
    {
        public abstract bool IsPayable { get; }
        public abstract void PayCost();
    }
}