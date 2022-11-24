using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{

    public class ConstructionSystem : MonoBehaviour
    {
        [SerializeField]
        Player _player;
        [SerializeField]
        private ConstructionLibrary constructionLibrary;
        [SerializeField]
        int _selectedTemplate = 0;

        public bool isBuilding = true;
        [SerializeField]
        private float _placeRange;

        private WorldGrid _grid;

        private void Start()
        {

            _grid = WorldManager.instance.grid;
        }

        private void Update()
        {
            if (!isBuilding) return;

            HandleKeyInput();
            HandleMouseInput();
        }

        private void HandleKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Period))
            {
                _selectedTemplate ++;

                if (_selectedTemplate == constructionLibrary.constructionTemplates.Length)
                {
                    _selectedTemplate = 0;
                }
            }
        }

        private void HandleMouseInput()
        {
            ConstructionTemplate template = constructionLibrary.constructionTemplates[_selectedTemplate];

            if (!GetMousePosition(out Vector2 position, _placeRange)) return;

            Vector2Int gridPosition = new (
                    Mathf.RoundToInt(position.x / WorldManager.GRIDSCALE) + WorldManager.gridOffset,
                    Mathf.RoundToInt(position.y / WorldManager.GRIDSCALE) + WorldManager.gridOffset);

            bool canBuild = WorldManager.instance.TryPreviewAt(
                gridPosition,
                template);

            if (!canBuild) return; 
         
            if (!Input.GetMouseButtonDown(0)) return;

            WorldManager.instance.PlaceConstruction(gridPosition, template, _player.inventory);
        }

        protected bool GetMousePosition(out Vector2 position, float maxDistance)
        {
            Camera camera = _player.mainCamera;
            position = new();

            Plane gridPlane = new(Vector3.up, new Vector3());

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (gridPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPosition = ray.GetPoint(distance);

                position = new Vector2(hitPosition.x, hitPosition.z);
                return true;
            }

            return false;
        }

        
    }
}