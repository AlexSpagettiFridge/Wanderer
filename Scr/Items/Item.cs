using System.Text.Json;
using System.Text.Json.Nodes;

namespace Wanderer.Items
{
    internal class Item
    {
        private ItemHandler itemHandler;
        private string specificData = "";
        public ItemHandler GetHandler() => itemHandler;

        public int Id { get => itemHandler.Id; }
        public string SpecificData { get => specificData; set => specificData = value; }

        public Item(ItemHandler itemHandler)
        {
            this.itemHandler = itemHandler;
        }

        public static Item CreateFromJson(JsonNode node) => CreateFromJson(JsonSerializer.SerializeToDocument(node));

        public static Item CreateFromJson(JsonDocument document) => CreateFromJson(document.RootElement);
        public static Item CreateFromJson(JsonElement json)
        {
            if (!json.TryGetProperty("Id", out JsonElement idElement))
            {
                return null;
            }
            int id = idElement.GetInt32();
            Item jsonItem = new Item(ItemBank.GetHandler((x) => x.Id == id));
            if(json.TryGetProperty("SpecificData",out JsonElement specificDataElement))
            {
                jsonItem.SpecificData = specificDataElement.GetString();
            }
            return jsonItem;
        }

        public JsonNode GetJson() => JsonSerializer.SerializeToNode(this);

        public override bool Equals(object obj)
        {
            if (obj is Item compObj)
            {
                return (compObj.GetHandler()==itemHandler&&compObj.SpecificData==specificData);
            }
            return false;
        }

        public override string ToString()
        {
            return GetHandler().Name;
        }
    }
}