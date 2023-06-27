using System.Collections.Generic;
using Wanderer.Abilities;

namespace Wanderer.Items.GameItems
{
    internal abstract class ItemEquipment : ItemHandler
    {
        public virtual List<Ability> AllowAbilities(Item item) => null;
        public virtual void Equip(Item item) { }
        public virtual void Unequip(Item item) { }
    }
}