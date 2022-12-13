using UnityEngine;

namespace ReganAlchemy
{
    [CreateAssetMenu(menuName = "Regan/Alchemy/Item")]
    public class Item : ScriptableObject
    {
        public string itemId = "new_item";
        public int quantity = 1;
        public int maxQuantity = 100;
        public string itemName = "new item";
        public Sprite sprite = null;
        public GameObject prefab;
        public int sellPrice = 1;
    }
}