using UnityEngine;

namespace ReganAlchemy
{

    public class ConstructionSystem : MonoBehaviour
    {
        public delegate void ChangeCraftingTipDelegate(string tip);
        public static event ChangeCraftingTipDelegate ChangeCraftingTip;

        [SerializeField]
        Player _player;
        [SerializeField]
        private ConstructionLibrary constructionLibrary;
        [SerializeField]
        int _selectedTemplate = 0;

        public bool isBuilding = false;
        [SerializeField]
        private float _placeRange;
        private int _rotationIndex = 0;



        private void Start()
        {

        }

        private void Update()
        {
            HandleKeyInput();

            if (!isBuilding) 
            {
                ChangeCraftingTip?.Invoke("(b) to build");
                return;
            }
            

            ChangeCraftingTip?.Invoke(CraftingRequirmentsString(constructionLibrary.constructionTemplates[_selectedTemplate]));

            HandleMouseInput();
        }

        private string CraftingRequirmentsString(ConstructionTemplate template)
        {
            string craftingString = $"{template.name} requires:\n";

            foreach (ItemStack itemStack in template.requiredItems)
            {
                craftingString += $"{itemStack.item.itemName}: {itemStack.quantity} \n";
            }

            craftingString += "-----------\n(.) next machine\n(,) rotate";

            return craftingString;
        }

        private void HandleKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleBuilding();
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                _selectedTemplate++;

                if (_selectedTemplate == constructionLibrary.constructionTemplates.Length)
                {
                    _selectedTemplate = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Comma))
            {
                _rotationIndex++;

                if (_rotationIndex == 4)
                {
                    _rotationIndex = 0;
                }
            }
        }

        private void ToggleBuilding()
        {
            if (!isBuilding)
            {
                isBuilding = true;
                return;
            }

            isBuilding = false;
            GameManager.instance.RemovePreview();
        }

        private void HandleMouseInput()
        {
            ConstructionTemplate template = constructionLibrary.constructionTemplates[_selectedTemplate];

            if (!GetMousePosition(out Vector2 position, _placeRange)) return;

            Vector2Int gridPosition = new(
                    Mathf.RoundToInt(position.x / GameManager.GRIDSCALE) + GameManager.gridOffset,
                    Mathf.RoundToInt(position.y / GameManager.GRIDSCALE) + GameManager.gridOffset);

            bool canBuild = GameManager.instance.TryPreviewAt(
                gridPosition,
                template,
                _rotationIndex);

            if (!canBuild)
            {
                if (!Input.GetMouseButtonDown(1)) return;

                GameManager.instance.RemoveConstruction(gridPosition);

                return;
            }
               

            if (!Input.GetMouseButtonDown(0)) return;

            GameManager.instance.PlaceConstruction(gridPosition, template, _player.inventory, _rotationIndex);
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