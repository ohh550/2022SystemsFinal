using UnityEngine;

namespace ReganAlchemy
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton Setup
        public static GameManager instance;

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

        public delegate void MoneyChangeDelegate(int money);
        public static event MoneyChangeDelegate OnMoneyChanged;

        public const int GRIDSCALE = 5;
        public static int gridOffset = 50;

        public WorldGrid grid;
        public Transform worldTransform;

        public ItemLibrary itemLibrary;

        [SerializeField]
        private int _gridSize = 100;


        private GameObject _previewObject;
        private ConstructionTemplate _previewTemplate;

        private int money;


        private void OnEnable()
        {
            Shop.ShopSell += OnMoneyChange;
        }

        private void OnDisable()
        {
            Shop.ShopSell -= OnMoneyChange;
        }

        private void Start()
        {
            gridOffset = _gridSize / 2;
            grid = new(100);
        }

        public void PlaceConstruction(Vector2Int position, ConstructionTemplate template, PlayerInventory inventory, int rotationIndex)
        {
            if (!RemoveResources(template, inventory)) return;

            GameObject construction = Instantiate(template.gamePrefab, worldTransform);

            construction.transform.position = new Vector3((position.x - gridOffset) * GRIDSCALE, 0, (position.y - gridOffset) * GRIDSCALE);
            
            Vector3 prefabRotation = construction.transform.rotation.eulerAngles;
            construction.transform.rotation = Quaternion.Euler(prefabRotation.x, prefabRotation.y , rotationIndex * 90);

            GridObject gridObject = construction.GetComponent<GridObject>();
            gridObject.gridPosition = position;

            grid.gridObjects[position.x, position.y] = gridObject;
        }

        public void RemoveConstruction(Vector2Int position)
        {
            if (!grid.IsOnGrid(position))
            {
                RemovePreview();
                return;
            }

            GridObject gridObject = grid.gridObjects[position.x, position.y];

            Destroy(gridObject.gameObject);
            grid.gridObjects[position.x, position.y] = null;
        }

        public bool IsEmpty(Vector2Int position)
        {
            if (!grid.IsOnGrid(position))
            {
                return false;
            }

            GridObject gridObject = grid.gridObjects[position.x, position.y];

            if (gridObject != null) return false;

            return true;
        }

        public bool TryPreviewAt(Vector2Int position, ConstructionTemplate template, int rotationIndex)
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
                _previewObject.transform.position = new Vector3((position.x - gridOffset) * GRIDSCALE, 0, (position.y - gridOffset) * GRIDSCALE);

                Vector3 prefabRotation = _previewObject.transform.rotation.eulerAngles;
                _previewObject.transform.rotation = Quaternion.Euler(prefabRotation.x, rotationIndex * 90, prefabRotation.z);

                if (_previewTemplate == template) { return true; }
            }

            RemovePreview();
            ShowPreview(template, position, rotationIndex);

            return true;
        }

        void ShowPreview(ConstructionTemplate template, Vector2Int position, int rotationIndex)
        {
            _previewObject = Instantiate(template.previewPrefab, worldTransform);
            _previewObject.transform.position = new Vector3(position.x * GRIDSCALE, 0, position.y * GRIDSCALE);

            Vector3 prefabRotation = _previewObject.transform.rotation.eulerAngles;
            _previewObject.transform.rotation = Quaternion.Euler(prefabRotation.x, rotationIndex * 90, prefabRotation.z);

            _previewTemplate = template;
        }

        public void RemovePreview()
        {
            Destroy(_previewObject);
            _previewObject = null;
            _previewTemplate = null;
        }

        public bool TryPreviewAt(Vector2 position, ConstructionTemplate template, int rotationIndex)
        {
            return TryPreviewAt(Vector2Int.RoundToInt(position), template, rotationIndex);
        }

        private bool RemoveResources(ConstructionTemplate template, PlayerInventory inventory)
        {
            ItemStack[] requiredItems = template.requiredItems;

            foreach (ItemStack itemStack in requiredItems)
            {
                if (inventory.GetItemAmount(itemStack.item.itemId) < itemStack.quantity) return false;
            }

            foreach (ItemStack itemStack in requiredItems)
            {
                inventory.RemoveItemAmount(itemStack.item.itemId, itemStack.quantity);
            }

            return true;
        }

        private void OnMoneyChange(int amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
        }
    }
}

