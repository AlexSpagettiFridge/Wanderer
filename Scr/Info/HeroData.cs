using System;
using System.Collections.Generic;
using Godot;
using Wanderer.Abilities;
using Wanderer.Items;
using Wanderer.Items.GameItems;

namespace Wanderer.Info
{
    internal class HeroData
    {
        public HeroResource Hp = new HeroResource(200),Stamina = new HeroResource(200);
        public ItemStorage Storage;
        public ItemStorage Equipment;
        public Ability[] Abilities;

        internal HeroData()
        {
            Abilities = new Ability[5];
            for(int i=0;i<5;i++)
            {
                Abilities[i] = null;
            }
            ItemBank.Init();
            Storage = new ItemStorage(12);
            Equipment = new ItemStorage(6);

            Storage.GiveItem(ItemBank.GetHandlerByName("Sword").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("Test").CreateItem());
        }

        private List<Predicate<Item>> GetEquipmentPredicates()
        {
            List<Predicate<Item>> result = new List<Predicate<Item>>();
            Predicate<Item> weaponSlotPredicate = (x) => x.GetHandler() is WeaponHandler;
            for (int i = 0; i < 3; i++)
            {
                result.Add(weaponSlotPredicate);
            }
            return result;
        }

    }
}