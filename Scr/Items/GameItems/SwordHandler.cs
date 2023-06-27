using System.Collections.Generic;
using Godot;
using Wanderer.Abilities;
using Wanderer.Entities;

namespace Wanderer.Items.GameItems
{
    internal class ItemSword : ItemWeapon
    {
        private readonly SpriteFrames spriteFrames = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Items.tres");
        public override string Name => "Sword";

        public override void Attack(Hero hero, Item item)
        {
            GD.Print("Swert -|======>");
        }

        public override Texture2D GetTexture(Item item) => spriteFrames.GetFrameTexture("Items32", 3);

        public override List<Ability> AllowAbilities(Item item)
        {
            List<Ability> result = new List<Ability>();
            result.Add(AbilityBank.GetByName("Attack").Create(item));
            return result;
        }
    }
}