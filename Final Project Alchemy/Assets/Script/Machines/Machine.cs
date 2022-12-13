using ReganAlchemy;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace ReganAlchemy
{
    public class Machine : GridObject
    {
        [SerializeField]
        Inventory _inventory;

        [SerializeField]
        DepositFrame[] _linkedDepositFrames;

        [SerializeField]
        Transform _outputTransform;

        [SerializeField]
        ItemStack[] _requiredResources;

        [SerializeField]
        int _maxPerStack = 4;

        [SerializeField]
        Item _product;

        [SerializeField]
        int _cooldown = 4;

        [SerializeField]
        AudioSource _audioSource;   

        bool _isProducing = false;

        private void Start()
        {
            _inventory = new Inventory();
            _inventory.maxPerStack = _maxPerStack;

            foreach (var frame in _linkedDepositFrames)
            {
                frame.linkedInventory = _inventory;
            }
        }

        private void Update()
        {
            if (_isProducing) return;
            TryProduce();
        }

        private void TryProduce()
        {

            foreach (ItemStack itemStack in _requiredResources)
            {
                
                if (_inventory.GetItemAmount(itemStack.item.itemId) < itemStack.quantity) return;
            }

            foreach (ItemStack itemStack in _requiredResources)
            {
                _inventory.RemoveItemAmount(itemStack.item.itemId, itemStack.quantity);
            }

            StartCoroutine(Produce());
        }

        private IEnumerator Produce()
        {
            _isProducing = true;

            yield return new WaitForSeconds(_cooldown);

            _isProducing = false;
            _audioSource.Play();
            EjectPrduct();
        }

        private void EjectPrduct()
        {
            GameObject ItemObject = Instantiate(_product.prefab);
            ItemObject.transform.position = _outputTransform.position;

        }
    }
}