using Godot;
using Wanderer.Entities;
using Wanderer.Items;

namespace Wanderer.Abilities
{
    internal abstract class AbilityHandler
    {
        protected static SpriteFrames defaultIcons = ResourceLoader.Load<SpriteFrames>("res://Gfx/Skills/SkillFrames.tres");
        protected readonly string name;
        public string Name => name;
        public abstract void Invoke(Ability ability, Hero hero);
        public virtual void Release(Ability abilty, Hero hero) { }
        public abstract AbilityCost GetCost(Ability ability);
        public abstract float GetCooldown(Ability ability);
        public abstract Texture2D GetIconTexture(Ability ability);

        protected AbilityHandler(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a Item using this AbilityHandler.
        /// </summary>
        /// <param name="sourceItem"></param>
        /// <returns></returns>
        public Ability Create(Item sourceItem = null) => new Ability(this, sourceItem);
    }
}