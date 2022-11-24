using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        Material _highlightMaterial;

        [SerializeField]
        MeshRenderer _meshRenderer;
        Material _originalMaterial;

        private void Start()
        {
            _originalMaterial = _meshRenderer.material;
        }

        public virtual void Highlight(bool highlighted)
        {
            if (!_meshRenderer) return;

            if (highlighted)
            {
                _meshRenderer.material = _highlightMaterial;
                return;
            }

            _meshRenderer.material = _originalMaterial;
        }



        public virtual void Interact(InteractionController interactionController)
        {

        }
    }
}
