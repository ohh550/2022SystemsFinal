using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ReganAlchemy
{
    [Serializable]
    public class DepositFrame : Interactable
    {
        [DoNotSerialize]
        public Inventory linkedInventory;

        public Item acceptableItem;
        public bool active = true;

        string startTooltip;

        private void Start()
        {
            startTooltip = _toolTip;
        }

        public override void Highlight(bool highlighted, InteractionController interactionController)
        {
            if (!active) return;

            base.Highlight(highlighted, interactionController);

            if (acceptableItem || !highlighted)
            {
                return;
            }

            Item item = interactionController.player.inventory.inventory.GetRandom();

            if (!item)
            {
                _toolTip = "Nothing to deposit";
                
                return;
            }

            _toolTip = $"(F) Deposit {interactionController.player.inventory.inventory.GetRandom().itemName}";
            
            return;
        }
        
        public override void Interact(InteractionController interactionController)
        {
            if (!active) return;

            if (acceptableItem == null)
            {
                GetRandomItem(interactionController);
                return;
            }

            GetAcceptedItem(interactionController);
        }

        public void GetRandomItem(InteractionController interactionController)
        {
            PlayerInventory inventory = interactionController.player.inventory;
            Item item = inventory.inventory.GetRandom();
            if (!item)
            {
                FlashFailed();
                return;
            }

            if (!linkedInventory.AddItem(item))
            {
                FlashFailed();
                return;
            }

            inventory.RemoveItemAmount(item.itemId, 1);
        }

        public void GetAcceptedItem(InteractionController interactionController)
        {
            PlayerInventory inventory = interactionController.player.inventory;

            if (inventory.GetItemAmount(acceptableItem.itemId) == 0)
            {
                FlashFailed();
                return;
            }

            if (!linkedInventory.AddItem(acceptableItem))
            {
                FlashFailed();
                return;
            }
            
            inventory.RemoveItemAmount(acceptableItem.itemId, 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            Gatherable gatherable = other.GetComponent<Gatherable>();

            if (!gatherable) return;

            if (!acceptableItem)
            {
                gatherable.Gather(linkedInventory);
                return;
            }

            if (gatherable.ItemType() != acceptableItem.itemId) return;

            gatherable.Gather(linkedInventory);
        }
    }
}
