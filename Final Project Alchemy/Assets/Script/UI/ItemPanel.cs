using ReganAlchemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace ReganAlchemy
{
    public class ItemPanel : MonoBehaviour
    {
        [SerializeField]
        Image _itemImage;
        [SerializeField]
        TextMeshProUGUI _quantityText;

        public void SetItem(ItemStack itemStack)
        {
            _itemImage.sprite = itemStack.item.sprite;
            _quantityText.text = itemStack.quantity.ToString();
        }
    }
}