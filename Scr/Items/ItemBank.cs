using Godot;
using System.Collections.Generic;
using Wanderer.Items.GameItems;

namespace Wanderer.Items
{
    static internal class ItemBank
    {
        private static List<ItemHandler> handlers;

        internal static void Init()
        {
            handlers = new List<ItemHandler>();
            SpriteFrames itemSprites = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Items.tres");

            AddItemHandler(new BasicItemHandler("Test",itemSprites.GetFrameTexture("Items32",0)));
        }

        public static void AddItemHandler(ItemHandler handler)
        {
            handlers.Add(handler);
            handler.Id = handlers.Count - 1;
        }
    }
}