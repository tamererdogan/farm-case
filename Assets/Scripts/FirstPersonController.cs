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
        if (nextAngle > 30 || nextAngle < -30)
            return;
        Camera.main.transform.rotation *= Quaternion.Euler(desiredAngle, 0, 0);
    }

    private void PickButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, clickDistance))
        {
            if (hit.collider.tag != "CollectableItem")
                return;

            int collectId = -1;
            if (hit.collider.name.Contains("Spade"))
                collectId = 0;
            if (hit.collider.name.Contains("Hoe"))
                collectId = 1;
            if (hit.collider.name.Contains("Scythe"))
                collectId = 2;

            if (collectId == -1)
                return;

            InventoryManager.Instance.AddItem(collectId);
            Destroy(hit.collider.gameObject);
        }
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
