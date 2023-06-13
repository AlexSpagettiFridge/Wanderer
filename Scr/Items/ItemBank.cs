using Godot;
using System.Collections.Generic;
using Wanderer.Items.GameItems;
using System.Linq;
using System;

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
            AddItemHandler(new BasicItemHandler("SmallTest",itemSprites.GetFrameTexture("Items32",0),true));
        }

        public static void AddItemHandler(ItemHandler handler)
        {
            handlers.Add(handler);
            handler.Id = handlers.Count - 1;
        }

        public static ItemHandler GetHandler(Predicate<ItemHandler> predicate) => handlers.Find(predicate);
        public static List<ItemHandler> GetHandlers(Predicate<ItemHandler> predicate) => handlers.FindAll(predicate);
        public static ItemHandler GetHandlerByName(string name) => GetHandler((x) => x.Name == name);
    }
}