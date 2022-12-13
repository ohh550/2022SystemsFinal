using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ReganAlchemy
{
    public class Interactable : MonoBehaviour
    {
        public delegate void ToolTipDelegate (string toolTip);
        public static event ToolTipDelegate SetToolTip;

        [SerializeField]
        protected string _toolTip = "(F)";

        [SerializeField]
        Material _highlightMaterial;
        [SerializeField]
        Material _failedMaterial;

        [SerializeField]
        MeshRenderer _meshRenderer;
        [SerializeField]
        int _materialIndex = 0;
        Material _originalMaterial;
        bool _isHighlighted = false;

        public virtual void Highlight(bool highlighted, InteractionController interactionController)
        {
            if (!_meshRenderer) return;

            if (!_originalMaterial) _originalMaterial = _meshRenderer.materials[_materialIndex];

            Material[] materials = _meshRenderer.materials;

            if (highlighted)
            {
                materials[_materialIndex] = _highlightMaterial;
                _meshRenderer.materials = materials;
                SetToolTip?.Invoke(_toolTip);
                _isHighlighted = true;
                return;
            }

            
            materials[_materialIndex] = _originalMaterial;
            _meshRenderer.materials = materials;
            SetToolTip?.Invoke("");
            _isHighlighted = false;
        }



        public virtual void Interact(InteractionController interactionController)
        {

        }

        public async void FlashFailed()
        {
            if (!_meshRenderer) return;

            Material[] materials = _meshRenderer.materials;

            materials[_materialIndex] = _failedMaterial;
            _meshRenderer.materials = materials;

            await Task.Delay(1000);

            materials[_materialIndex] = _originalMaterial;
            _meshRenderer.materials = materials;
        }

        private void OnDestroy()
        {
            if (!_isHighlighted) return;
            SetToolTip?.Invoke("");
        }
    }
}
