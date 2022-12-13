using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    public class PlayerInventory : MonoBehaviour
    {
        public delegate void PlayerInventoryUpdate(PlayerInventory playerInventory);
        public static event PlayerInventoryUpdate OnInventoryUpdated;

        public Inventory inventory;

        [SerializeField]
        int _maxItems;
        [SerializeField]
        int _maxItemsPerStack = 10;

        private void Awake()
        {
            inventory = new Inventory
            {
                maxItems = _maxItems,
                maxPerStack = _maxItemsPerStack,
                OnUpdateMethod = SignalUpdate
            };
        }

        public bool HasItem(string itemName)
        {
            return inventory.HasItem(itemName);
        }

        public int GetItemAmount(string itemId)
        {
            return inventory.GetItemAmount(itemId);
        }

        public bool RemoveItemAmount(string itemId, int amount)
        {
            return inventory.RemoveItemAmount(itemId, amount);
        }

        public bool AddItem(Item item)
        {
            return inventory.AddItem(item);
        }

        public bool AddItemStack(ItemStack itemStack)
        {
            return inventory.AddItemStack(itemStack);
        }

        public bool Gather(Item item)
        {
            if (!AddItem(item)) return false;

            return true;
        }

        public bool Gather(ItemStack itemStack)
        {
            if (!AddItemStack(itemStack)) return false;

            return true;
        }

        private void SignalUpdate()
        {
            OnInventoryUpdated?.Invoke(this);
        }
    }

    
}