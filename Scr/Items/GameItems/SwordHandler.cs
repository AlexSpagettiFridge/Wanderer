using Godot;
using Wanderer.Entities;

namespace Wanderer.Items.GameItems
{
    internal class SwordHandler : WeaponHandler
    {
        private readonly SpriteFrames spriteFrames = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Items.tres");
        public override string Name => "Sword";

        public override void Attack(Hero hero, Item item)
        {
            GD.Print("Swert -|======>");
        }

        public override Texture2D GetTexture() => spriteFrames.GetFrameTexture("Items32", 3);
    }
}