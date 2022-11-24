using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    public class InteractionController : MonoBehaviour
    {

        public Player player;

        [SerializeField]
        float _gatherRadius = 5;
        [SerializeField]
        LayerMask _interactableMask;

        Interactable _highlightedInteractable = null;

        private void Update()
        {
            ScanObjects();
            HandleInput();
        }

        private void HandleInput()
        {
            if (!Input.GetKeyDown(KeyCode.F)) return;

            if (!_highlightedInteractable) return;

            _highlightedInteractable.Interact(this);
        }

        private void ScanObjects()
        {
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, _gatherRadius, _interactableMask);

            Interactable closestInteractable = GetClosestCollider(nearbyObjects)?.GetComponent<Interactable>();

            if (closestInteractable == null)
            {
                _highlightedInteractable?.Highlight(false);
                _highlightedInteractable = null;
                return;
            }

            if (closestInteractable == _highlightedInteractable) return;

            _highlightedInteractable?.Highlight(false);
            closestInteractable.Highlight(true);
            _highlightedInteractable = closestInteractable;
        }

        private Collider GetClosestCollider(Collider[] colliders)
        {
            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;

            foreach(Collider collider in colliders)
            {
                float sqrMagnitude = (collider.transform.position - transform.position).sqrMagnitude;

                if (sqrMagnitude < closestDistance)
                {
                    closestDistance = sqrMagnitude;
                    closestCollider = collider;
                }
            }

            return closestCollider;
        }
    }
}
