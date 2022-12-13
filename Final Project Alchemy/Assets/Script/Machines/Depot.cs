using ReganAlchemy;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace ReganAlchemy
{
    public class Depot : GridObject
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
        Item _product;

        bool _cooldown = false;

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

            if (_cooldown) return;
        }



        private IEnumerator Dispence()
        {
            _cooldown = true;

            Item item = _inventory.RemoveRandom();

            if (!item) yield break;

            EjectItem(item);

            yield return new WaitForSeconds(4);

            _cooldown = false;

        }

        private void EjectItem(Item item)
        {
            GameObject ItemObject = Instantiate(_product.prefab);
            ItemObject.transform.position = _outputTransform.position;
        }
    }
}