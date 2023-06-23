using Wanderer.Entities;

namespace Wanderer.Items.GameItems
{
    internal abstract class WeaponHandler : ItemHandler
    {
        public abstract void Attack(Hero hero, Item item);
    }
}