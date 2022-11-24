using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ReganAlchemy
{
    public class WorldManager : MonoBehaviour
    {
        #region Singleton Setup
        public static WorldManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            Destroy(gameObject);
        }
        #endregion

        public const int GRIDSCALE = 5;
        public static int gridOffset = 50;

        public WorldGrid grid;
        public Transform worldTransform;

        [SerializeField]
        private int _gridSize = 100;
        

        private GameObject _previewObject;
        private ConstructionTemplate _previewTemplate;


        private void Start()
        {
            gridOffset = _gridSize / 2;
            grid = new (100);
        }

        public void PlaceConstruction(Vector2Int position, ConstructionTemplate template, Inventory inventory)
        {
            if (!HasResources(template, inventory)) return;

            GameObject construction = Instantiate(template.gamePrefab, worldTransform);

            construction.transform.position = new Vector3((position.x - gridOffset) * GRIDSCALE, 0, (position.y - gridOffset) * GRIDSCALE);

            GridObject gridObject = new GridObject(position, construction);

            grid.gridObjects[position.x, position.y] = gridObject;
        }

        public bool TryPreviewAt(Vector2Int position, ConstructionTemplate template)
        {
            if (!grid.IsOnGrid(position))
            {
                RemovePreview();
                return false;
            }

            GridObject gridObject = grid.gridObjects[position.x, position.y];

            if (gridObject != null) return false;

            if (_previewObject != null)
            {
                _previewObject.transform.position = new Vector3((position.x - gridOffset) * GRIDSCALE , 0, (position.y - gridOffset) * GRIDSCALE);

                if (_previewTemplate == template) { return true; }
            }

            RemovePreview();
            ShowPreview(template, position);

            return true;
        }

        void ShowPreview(ConstructionTemplate template, Vector2Int position)
        {
            _previewObject = Instantiate(template.previewPrefab, worldTransform);
            _previewObject.transform.position = new Vector3(position.x * GRIDSCALE, 0, position.y * GRIDSCALE);
            _previewTemplate = template;
        }

        void RemovePreview()
        {
            Destroy(_previewObject);
            _previewObject = null;
            _previewTemplate = null;
        }

        public bool TryPreviewAt(Vector2 position, ConstructionTemplate template)
        {
            return TryPreviewAt(Vector2Int.RoundToInt(position), template);
        }

        private bool HasResources(ConstructionTemplate template, Inventory inventory)
        {
            Debug.LogAssertion("HasResources not implemented yet, no resources required for construction.");
            return true;
        }
    }
}

