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
        [SerializeField]
        int _materialIndex = 0;
        Material _originalMaterial;

        private void Start()
        {
            _originalMaterial = _meshRenderer.materials[_materialIndex];
        }

        public virtual void Highlight(bool highlighted)
        {
            if (!_meshRenderer) return;

            Material[] materials = _meshRenderer.materials;

            if (highlighted)
            {
                materials[_materialIndex] = _highlightMaterial;
                _meshRenderer.materials = materials;
                return;
            }

            
            materials[_materialIndex] = _originalMaterial;
            _meshRenderer.materials = materials;
        }



        public virtual void Interact(InteractionController interactionController)
        {

        }
    }
}
