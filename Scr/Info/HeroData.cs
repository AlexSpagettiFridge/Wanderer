using System.Collections.Generic;
using Godot;
using Wanderer.Items;

namespace Wanderer.Info
{
    internal class HeroData
    {
        private int hp;

        public int Hp { get => hp; set => hp = value; }
        public  ItemStorage Storage;
        

        internal HeroData()
        {
            ItemBank.Init();
            Storage = new ItemStorage(12);
            Storage.GiveItem(ItemBank.GetHandlerByName("Test").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("Test").CreateItem());
        }

        
    }
}