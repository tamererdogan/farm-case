using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    #region SINGLETON
    public static FirstPersonController Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float xRotationSensivity;

    [SerializeField]
    private float yRotationSensivity;

    [SerializeField]
    private GameObject inventoryUI;

    [SerializeField]
    private GameObject marketUI;

    private PlayerInput playerInput;

    private Rigidbody rigidBody;

    [SerializeField]
    private float clickDistance;

    void Start()
    {
        LockCursor();
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.Player.Move.Enable();
        playerInput.Player.Look.Enable();
        playerInput.Player.InventoryButton.Enable();
        playerInput.Player.MarketButton.Enable();
        playerInput.Player.PickButton.Enable();
        playerInput.Player.InventoryButton.performed += InventoryButtonClicked;
        playerInput.Player.MarketButton.performed += MarketButtonClicked;
        playerInput.Player.PickButton.performed += PickButtonClicked;
    }

    void Update()
    {
        Vector2 direction = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 movementVector = transform.forward * direction.y + transform.right * direction.x;
        rigidBody.velocity = movementVector * moveSpeed;

        if (IsUIOpen())
            return;

        Vector2 mouseDelta = playerInput.Player.Look.ReadValue<Vector2>();

        //X Rotation
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x * xRotationSensivity, 0);

        //Y Rotation
        float desiredAngle = -mouseDelta.y * yRotationSensivity;
        float nextAngle = Camera.main.transform.eulerAngles.x + desiredAngle;
        if (nextAngle > 180)
            nextAngle -= 360;
        if (nextAngle > 50 || nextAngle < -50)
            return;
        Camera.main.transform.rotation *= Quaternion.Euler(desiredAngle, 0, 0);
    }

    private void PickButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, clickDistance))
        {
            if (CollectItemCheck(hit))
                return;
            if (GardenActionCheck(hit))
                return;
        }
    }

    private bool GardenActionCheck(RaycastHit hit)
    {
        if (hit.collider.tag != "PlantField")
            return false;

        PlantField plantField = hit.collider.gameObject.GetComponent<PlantField>();
        if (plantField == null)
            return false;

        //Empty state
        if (plantField.GetState() == 0)
        {
            int? toolItemId = EquipManager.Instance.GetToolItemId();
            if (toolItemId == null)
            {
                Debug.Log("Ekim yapmak için alet gereklidir.");
                return false;
            }

            ItemSO toolItem = DataManager.Instance.GetItem((int)toolItemId);
            if (toolItem == null)
            {
                Debug.Log("Alet bulunamadı.");
                return false;
            }

            int? seedItemId = EquipManager.Instance.GetSeedItemId();
            if (seedItemId == null)
            {
                Debug.Log("Ekim yapmak için tohum gereklidir.");
                return false;
            }

            ItemSO seedItem = DataManager.Instance.GetItem((int)seedItemId);
            if (seedItem == null)
            {
                Debug.Log("Tohum bulunamadı.");
                return false;
            }

            EquipManager.Instance.EquipSeed(null);
            plantField.Plant((SeedSO)seedItem, ((ToolSO)toolItem).plantTimeBoost);
        }

        return true;
    }

    private bool CollectItemCheck(RaycastHit hit)
    {
        if (hit.collider.tag != "CollectableItem")
            return false;

        int collectId = -1;

        if (hit.collider.name == "Spade(Clone)")
            collectId = 0;
        if (hit.collider.name == "Hoe(Clone)")
            collectId = 1;
        if (hit.collider.name == "Scythe(Clone)")
            collectId = 2;
        if (hit.collider.name == "BroccoliSeed(Clone)")
            collectId = 3;
        if (hit.collider.name == "CarrotSeed(Clone)")
            collectId = 4;
        if (hit.collider.name == "PotatoSeed(Clone)")
            collectId = 5;
        if (hit.collider.name == "Broccoli(Clone)")
            collectId = 6;
        if (hit.collider.name == "Carrot(Clone)")
            collectId = 7;
        if (hit.collider.name == "Potato(Clone)")
            collectId = 8;

        if (collectId == -1)
            return false;

        InventoryManager.Instance.AddItem(collectId);
        Destroy(hit.collider.gameObject);
        return true;
    }

    private void InventoryButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        InventoryManager.Instance.ToggleUI();
        if (IsUIOpen())
            UnlockCursor();
        else
            LockCursor();
    }

    private void MarketButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MarketManager.Instance.ToggleUI();
        if (IsUIOpen())
            UnlockCursor();
        else
            LockCursor();
    }

    private bool IsUIOpen()
    {
        return InventoryManager.Instance.IsOpen() || MarketManager.Instance.IsOpen();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
