using Wanderer.Entities;

namespace Wanderer.Items.GameItems
{
    internal abstract class ItemWeapon : ItemEquipment
    {
        public abstract void Attack(Hero hero, Item item);
    }
}