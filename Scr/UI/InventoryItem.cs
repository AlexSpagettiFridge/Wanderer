using Godot;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Wanderer.Items;

namespace Wanderer.Ui
{
    internal partial class InventoryItem : Control
    {
        private static SpriteFrames frameSprite = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Frame.tres");
        private readonly Vector2 margin = new Vector2(4, 4);
        private InventorySlot inventorySlot = null;
        private string animation = "mono";

        public InventoryItem()
        {
            inventorySlot = new InventorySlot();
            inventorySlot.ItemChanged += OnItemChanged;
        }

        public InventoryItem(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            inventorySlot.ItemChanged += OnItemChanged;
            OnItemChanged(this, null);
        }

        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(64, 64);
            Draw += _Draw;
        }

        private void OnItemChanged(object sender, InventorySlotChangedArgs args)
        {
            animation = inventorySlot.IsQuadSlot ? "quad" : "mono";
            QueueRedraw();
        }

        public override void _Draw()
        {
            DrawTextureRect(frameSprite.GetFrameTexture(animation, 0), new Rect2(Vector2.Zero, Size), false);
            if (inventorySlot == null) { return; }
            if (inventorySlot.IsQuadSlot)
            {
                Vector2 quadSize = Size / 2 - new Vector2(6, 6);
                Vector2[] texturePosition = new Vector2[] {
                    new Vector2(2, 2),
                    new Vector2(Size.X - quadSize.X-2, 2),
                    new Vector2(2,Size.Y - quadSize.Y-2),
                    Size-quadSize-new Vector2(2,2)
                };
                for (int i = 0; i < inventorySlot.Items.Length; i++)
                {
                    if (inventorySlot.Items[i] != null)
                    {
                        Texture2D quadTexture = inventorySlot.Items[i].IconTexture;
                        DrawTextureRect(quadTexture, new Rect2(texturePosition[i], quadSize), false);
                    }
                }
                return;
            }
            if (inventorySlot.Items[0] == null) { return; }
            Texture2D itemTexture = inventorySlot.Items[0].IconTexture;

            DrawTextureRect(itemTexture, new Rect2(new Vector2(2, 2), Size - new Vector2(4, 4)), false);
        }

        #region Drag & Drop
        public override void _DropData(Vector2 atPosition, Variant data)
        {
            //Read Json
            JsonElement root = JsonDocument.Parse((string)data).RootElement;
            if (!root.TryGetProperty("Item", out JsonElement itemJson)) { return; }
            if (!root.TryGetProperty("ControlPath", out JsonElement pathJson)) { return; }
            Item droppedItem = Item.CreateFromJson(itemJson);
            InventoryItem tradeSlot = GetNode<InventoryItem>(new NodePath(pathJson.ToString()));

            InventorySlot.TryInsertResult insertResult = inventorySlot.TryInsertItem(droppedItem);
            //Either successfull Trade of unmet requirements will end function
            if (insertResult == InventorySlot.TryInsertResult.Success ||
                insertResult == InventorySlot.TryInsertResult.DoesNotMeet)
            {
                tradeSlot.inventorySlot.RemoveItem(droppedItem);
                return;
            }
            //Try swapping Items


            Item localItem;
            if (insertResult == InventorySlot.TryInsertResult.DoesNotMeet)
            {
                //Trade with only item in InventorySlot
                int quadItemAmount = inventorySlot.IsQuadSlot ? inventorySlot.ItemAmount : tradeSlot.inventorySlot.ItemAmount;
                if (quadItemAmount != 1)
                {
                    return;
                }
                localItem = inventorySlot.Items.First(x => x != null);
            }
            else
            {
                //Trade with hovered Item
                localItem = inventorySlot.Items[GetItemIndexAtLocalPosition(atPosition)];
            }
            inventorySlot.TrySwappingItems(localItem, droppedItem, ref tradeSlot.inventorySlot);
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            return true;
        }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            Item item = inventorySlot.Items[GetItemIndexAtLocalPosition(atPosition)];
            if (item == null)
            {
                return "";
            }
            JsonNode jsonNode = JsonNode.Parse("{}");
            jsonNode["ControlPath"] = GetPath().ToString();
            jsonNode["Item"] = (JsonNode)item.GetJson();
            string serializedJson = JsonSerializer.Serialize(jsonNode);
            return serializedJson;
        }

        private int GetItemIndexAtLocalPosition(Vector2 atPosition)
        {
            if (inventorySlot.IsQuadSlot)
            {
                Vector2 quadVector = (atPosition / (Size / 2)).Floor();
                int itemIndex = (int)Math.Clamp(quadVector.X + quadVector.Y * 2, 0, 3);
                return itemIndex;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}