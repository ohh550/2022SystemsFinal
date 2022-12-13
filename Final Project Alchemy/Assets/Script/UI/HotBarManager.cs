using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    namespace ReganAlchemy
    {
        public class HotBarManager : MonoBehaviour
        {
            private Dictionary<string, ItemPanel> _itemPanels = new Dictionary<string, ItemPanel>();

            [SerializeField]
            GameObject itemPanelPrefab;

            private void OnEnable()
            {
                PlayerInventory.OnInventoryUpdated += OnInventoryUpdated;
            }

            private void OnDisable()
            {
                PlayerInventory.OnInventoryUpdated -= OnInventoryUpdated;
            }

            void OnInventoryUpdated(PlayerInventory playerInventory)
            {
                foreach (ItemPanel itemPanel in _itemPanels.Values)
                {
                    Destroy(itemPanel.gameObject);
                }

                _itemPanels = new Dictionary<string, ItemPanel>();

                foreach (ItemStack itemStack in playerInventory.inventory.itemStacks.Values)
                {
                    TryAddPanel(itemStack);
                }
            }

            private void TryAddPanel(ItemStack itemStack)
            {
                Item item = itemStack.item;
                if (_itemPanels.ContainsKey(item.itemId))
                {
                    _itemPanels[item.itemId].SetItem(itemStack);
                    return;
                }

                ItemPanel newPanel = InstantiatePanel();
                newPanel.SetItem(itemStack);
                _itemPanels.Add(item.itemId, newPanel);
            }

            private ItemPanel InstantiatePanel()
            {
                return Instantiate(itemPanelPrefab, transform).GetComponent<ItemPanel>();
            }
        }
    }
}
