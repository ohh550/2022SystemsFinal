using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    [CreateAssetMenu(menuName = "Regan/Alchemy/ItemLibrary")]
    public class ItemLibrary : ScriptableObject
    {
        public Item[] items = new Item[0];

        public Dictionary<string, Item> itemByString = new Dictionary<string, Item>();

        private void OnEnable()
        {
            foreach (Item item in items)
            {
                if (item == null) continue;

                if (itemByString.ContainsKey(item.itemId))
                {
                    Debug.LogWarning($"Item with id: \"{item.itemId}\" already exists, skipping!", this);
                    continue;
                }

                itemByString.Add(item.itemId, item);
            }
        }
    }
}