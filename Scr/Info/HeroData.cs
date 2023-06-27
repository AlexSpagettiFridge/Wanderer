using System;
using System.Collections.Generic;
using Wanderer.Abilities;
using Wanderer.Items;
using Wanderer.Items.GameItems;

namespace Wanderer.Info
{
    internal class HeroData
    {
        public HeroResource Hp = new HeroResource(200), Stamina = new HeroResource(200);
        public ItemStorage Storage;
        public ItemStorage Equipment;
        public Ability[] Abilities;

        internal HeroData()
        {
            Abilities = new Ability[5];
            for (int i = 0; i < 5; i++)
            {
                Abilities[i] = null;
            }
            Storage = new ItemStorage(12);
            Equipment = new ItemStorage(6);
            Equipment.SlotItemChanged += OnEquipmentSlotChanged;

            Storage.GiveItem(ItemBank.GetHandlerByName("Sword").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("SmallTest").CreateItem());
            Storage.GiveItem(ItemBank.GetHandlerByName("Test").CreateItem());
        }

        #region Equipment
        private void OnEquipmentSlotChanged(object sender, InventorySlotChangedArgs args)
        {
            if (args.Item == null) { return; }
            if (args.Item.GetHandler() is ItemEquipment handler)
            {
                switch (args.Change)
                {
                    case InventorySlotChangedArgs.ChangeType.Added:
                        handler.Equip(args.Item);
                        break;
                    case InventorySlotChangedArgs.ChangeType.Removed:
                        handler.Unequip(args.Item);
                        break;
                }
            }
        }

        private List<Predicate<Item>> GetEquipmentPredicates()
        {
            List<Predicate<Item>> result = new List<Predicate<Item>>();
            Predicate<Item> weaponSlotPredicate = (x) => x.GetHandler() is ItemWeapon;
            for (int i = 0; i < 3; i++)
            {
                result.Add(weaponSlotPredicate);
            }
            return result;
        }
        #endregion

        public Dictionary<string, List<Ability>> GetAbilitiesByCategory()
        {
            Dictionary<string, List<Ability>> result = new Dictionary<string, List<Ability>>();
            foreach (Item item in Equipment.GetAllItems())
            {
                if (item.GetHandler() is ItemEquipment equipment)
                {
                    List<Ability> allowedAbilities = equipment.AllowAbilities(item);
                    if (allowedAbilities != null)
                    {
                        result.TryAdd(item.HandlerName, allowedAbilities);
                    }
                }
            }
            return result;
        }
    }
}