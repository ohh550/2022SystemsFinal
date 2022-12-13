using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ReganAlchemy
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public delegate void SetTooltipDelegate(string tooltip);

        private void OnEnable()
        {
            Interactable.SetToolTip += SetTooltip;
        }

        private void OnDisable()
        {
            Interactable.SetToolTip -= SetTooltip;
        }

        public void SetTooltip(string tooltip)
        {
            _text.text = tooltip;
        }
    }
}