using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Godot;

namespace Wanderer.Items
{
    internal class Item
    {
        private static int nextId = 0;
        private readonly int id;
        private ItemHandler itemHandler;
        private string specificData = "";

        public int Id => id;
        public string HandlerName => itemHandler.Name;
        public string SpecificData { get => specificData; set => specificData = value; }
        [JsonIgnore]
        public Texture2D IconTexture => itemHandler.GetTexture(this);

        public Item(ItemHandler itemHandler, int id = -1)
        {
            this.itemHandler = itemHandler;
            this.id = id;
            if (id == -1) { this.id = nextId; }
            nextId = Math.Max(nextId, this.id + 1);
        }

        public ItemHandler GetHandler() => itemHandler;

        #region Json
        public static Item CreateFromJson(JsonNode node) => CreateFromJson(JsonSerializer.SerializeToDocument(node));
        public static Item CreateFromJson(JsonDocument document) => CreateFromJson(document.RootElement);
        public static Item CreateFromJson(JsonElement json)
        {
            if (!json.TryGetProperty("Id", out JsonElement idElement)) { return null; }
            int id = idElement.GetInt32();
            if (!json.TryGetProperty("HandlerName", out JsonElement handlerElement)) { return null; }
            string name = handlerElement.GetString();
            Item jsonItem = new Item(ItemBank.GetHandlerByName(name), id);
            if (json.TryGetProperty("SpecificData", out JsonElement specificDataElement))
            {
                jsonItem.SpecificData = specificDataElement.GetString();
            }
            return jsonItem;
        }

        public JsonNode GetJson() => JsonSerializer.SerializeToNode(this);
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is Item compObj)
            {
                return compObj.Id == Id;
            }
            return false;
        }

        public override string ToString()
        {
            return GetHandler().Name;
        }

        public override int GetHashCode() => Id;
    }
}