using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    public class Gatherable : Interactable
    {
        

        public override void Interact(InteractionController interactionController)
        {
            if (!interactionController.player.inventory.Gather()) return;

            Destroy(gameObject);
        }
    }
}

