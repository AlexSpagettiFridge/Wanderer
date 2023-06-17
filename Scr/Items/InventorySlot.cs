using System;
using System.Linq;
namespace Wanderer.Items
{
    internal class InventorySlot
    {
        private Item[] items = new Item[1];
        private Predicate<Item> requirements = null;
        public int ItemAmount => items.Count(x => x != null);

        public event EventHandler<Item[]> ItemChanged;

        public InventorySlot(Predicate<Item> requirements = null)
        {
            this.requirements = requirements;
        }

        public Item[] Items
        {
            get => items;
        }

        public bool DoesItemMeetRequirements(Item item) => requirements.Invoke(item);

        public TryInsertResult TryInsertItem(Item item)
        {
            if (requirements != null)
            {
                if (!DoesItemMeetRequirements(item)) { return TryInsertResult.DoesNotMeet; }
            }
            bool isItemSmall = item.GetHandler().IsSmall;
            if (IsQuadSlot && !isItemSmall) { return TryInsertResult.WrongSize; }

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null) { continue; }
                if (isItemSmall) { IsQuadSlot = true; }
                items[i] = item;
                ItemChanged?.Invoke(this, items);
                return TryInsertResult.Success;
            }

            return TryInsertResult.Full;
        }

        public void ForceInsertItem(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null) { continue; }
                if (item.GetHandler().IsSmall) { IsQuadSlot = true; }
                items[i] = item;
                ItemChanged?.Invoke(this, items);
                return;
            }
            RemoveItem(items[0]);
            if (item.GetHandler().IsSmall) { IsQuadSlot = true; }
            items[0] = item;
            ItemChanged?.Invoke(this, items);
        }

        public bool TrySwappingItems(Item localItem, Item tradeItem, ref InventorySlot tradeSlot)
        {
            if (!DoesItemMeetRequirements(tradeItem) || !tradeSlot.DoesItemMeetRequirements(localItem)) { return false; }
            //Find out if Slot Amount is Compatible;
            if (IsQuadSlot != tradeSlot.IsQuadSlot)
            {
                if (IsQuadSlot && ItemAmount > 1) { return false; }
                if (tradeSlot.IsQuadSlot && tradeSlot.ItemAmount > 1) { return false; }
            }
            RemoveItem(localItem);
            tradeSlot.RemoveItem(tradeItem);
            ForceInsertItem(tradeItem);
            tradeSlot.ForceInsertItem(localItem);
            return true;
        }

        public void RemoveItem(Item item, bool ignoreError = false)
        {
            bool isEmpty = true, isRemoved = false;
            for (int i = 0; i < Items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (items[i].Equals(item) && !isRemoved)
                    {
                        items[i] = null;
                        isRemoved = true;
                        continue;
                    }
                    isEmpty = false;
                }
                
            }
            if (isEmpty)
            {
                IsQuadSlot = false;
            }
            if (!isRemoved && !ignoreError) { throw new Exception($"Item '{item.ToString()}' for Removal not found"); }
            ItemChanged?.Invoke(this, items);
        }

        public bool IsQuadSlot
        {
            get => Items.LongLength == 4;
            set
            {
                if (value == IsQuadSlot) { return; }
                if (value)
                {
                    items = new Item[4];
                }
                else
                {
                    items = new Item[1];
                }
                for (int i = 0; i < items.Length; i++)
                {
                    items[0] = null;
                }
            }
        }

        public delegate bool CheckItem(Item item);

        public enum TryInsertResult
        {
            Success, DoesNotMeet, Full, WrongSize
        }
    }
}