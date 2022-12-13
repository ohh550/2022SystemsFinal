using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ReganAlchemy
{
    public class Inventory
    {
        public Action OnUpdateMethod;

        public int maxItems = 100;

        public int maxPerStack = 5;

        public Dictionary<string, ItemStack> itemStacks = new Dictionary<string, ItemStack>();

        public bool HasItem(string itemName)
        {
            return itemStacks.ContainsKey(itemName);
        }

        public int GetItemAmount(string itemId)
        {
            if (!HasItem(itemId)) return 0;

            return itemStacks[itemId].quantity;
        }

        public bool RemoveItemAmount(string itemId, int amount)
        {
            if (GetItemAmount(itemId) < amount) return false;

            itemStacks[itemId].quantity -= amount;

            if (itemStacks[itemId].quantity == 0)
            {
                itemStacks.Remove(itemId);
            }

            SignalUpdate();
            return true;
        }

        public bool AddItem(Item item)
        {
            if (HasItem(item.itemId))
            {
                int newQuantity = itemStacks[item.itemId].quantity + 1;
                int newTotalItemAmount = 1 + GetTotalItemAmount();

                if (newQuantity > item.maxQuantity) return false;

                if (newQuantity > maxPerStack) return false;

                if (newTotalItemAmount > maxItems) return false;

                itemStacks[item.itemId].quantity = newQuantity;

                SignalUpdate();
                return true;
            }
            ItemStack newStack = new ItemStack(item, 1);

            itemStacks.Add(item.itemId, newStack);
            SignalUpdate();
            return true;
        }

        public bool AddItemStack(ItemStack itemStack)
        {
            Item item = itemStack.item;
            if (HasItem(itemStack.item.itemId))
            {
                int newQuantity = itemStacks[item.itemId].quantity + itemStack.quantity;
                int newTotalItemAmount = itemStack.quantity + GetTotalItemAmount();

                if (newQuantity > item.maxQuantity) return false;

                if (newQuantity > maxPerStack) return false;

                if (newTotalItemAmount > maxItems) return false;

                itemStacks[item.itemId].quantity = newQuantity;
                SignalUpdate();
                return true;
            }

            ItemStack newStack = new ItemStack(item, 1);
            itemStacks.Add(item.itemId, newStack);
            SignalUpdate();
            return true;
        }

        public int GetTotalItemAmount()
        {
            int total = 0;

            foreach (ItemStack item in itemStacks.Values)
            {
                total += item.quantity;
            }

            return total;
        }

        private void SignalUpdate()
        {
            OnUpdateMethod?.Invoke();
        }

        public Item RemoveRandom()
        {
            if (GetTotalItemAmount() == 0) return null;

            Item item = itemStacks.ElementAt(0).Value.item;

            RemoveItemAmount(item.itemId, 1);

            return item;
        }

        public Item GetRandom()
        {
            if (GetTotalItemAmount() == 0) return null;

            return itemStacks.ElementAt(0).Value.item;
        }
    }

    [Serializable]
    public class ItemStack
    {
        public Item item;
        public int quantity;

        public ItemStack(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
    }
}

