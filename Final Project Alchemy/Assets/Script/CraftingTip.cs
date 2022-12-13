using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ReganAlchemy
{
    public class CraftingTip : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            ConstructionSystem.ChangeCraftingTip += SetTooltip;
        }

        private void OnDisable()
        {
            ConstructionSystem.ChangeCraftingTip -= SetTooltip;
        }

        public void SetTooltip(string tooltip)
        {
            _text.text = tooltip;
        }
    }
}