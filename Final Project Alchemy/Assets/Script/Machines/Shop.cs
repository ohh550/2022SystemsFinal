using ReganAlchemy;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
namespace ReganAlchemy
{
    public class Shop : GridObject
    {
        public delegate void ShopSellDelegate(int amount);
        public static event ShopSellDelegate ShopSell;

        [SerializeField]
        Inventory _inventory;

        [SerializeField]
        DepositFrame[] _linkedDepositFrames;

        [SerializeField]
        AudioSource _audioSource;

        private void Start()
        {
            _inventory = new Inventory();
            _inventory.maxItems = 20;

            foreach (var frame in _linkedDepositFrames)
            {
                frame.linkedInventory = _inventory;
            }
        }

        private void Update()
        {
            if (_inventory.GetTotalItemAmount() == 0) return;

            Sell();
        }



        private void Sell()
        {

            Item item = _inventory.RemoveRandom();

            if (!item) return;

            _audioSource.Play();

            ShopSell?.Invoke(item.sellPrice);
        }

    }
}