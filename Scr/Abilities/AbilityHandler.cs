namespace Wanderer.Abilities
{
    internal abstract class AbilityHandler
    {
        private int id = 0;
        protected string name;

        public string Name => name;
        public int Id { get => id; set => id = value; }


    }
}