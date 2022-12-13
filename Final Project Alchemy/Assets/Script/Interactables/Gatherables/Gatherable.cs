using UnityEngine;


namespace ReganAlchemy
{
    public class Gatherable : Interactable
    {
        [SerializeField]
        Item gatheredItem;

        public override void Interact(InteractionController interactionController)
        {
            if (!interactionController.player.inventory.Gather(gatheredItem)) return;

            Destroy(gameObject);
        }

        public void Gather(Inventory inventory)
        {
            if (!inventory.AddItem(gatheredItem)) return;

            Destroy(gameObject);
        }

        public string ItemType()
        {
            return gatheredItem.itemId;
        }
    }
}

