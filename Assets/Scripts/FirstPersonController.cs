using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
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

    void Start()
    {
        LockCursor();
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.Player.Move.Enable();
        playerInput.Player.Look.Enable();
        playerInput.Player.InventoryButton.Enable();
        playerInput.Player.MarketButton.Enable();
        playerInput.Player.InventoryButton.performed += InventoryButtonClicked;
        playerInput.Player.MarketButton.performed += MarketButtonClicked;
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

    private void InventoryButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        if (IsUIOpen())
            UnlockCursor();
        else
            LockCursor();
    }

    private void MarketButtonClicked(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        marketUI.SetActive(!marketUI.activeSelf);
        if (IsUIOpen())
            UnlockCursor();
        else
            LockCursor();
    }

    private bool IsUIOpen()
    {
        return inventoryUI.activeSelf || marketUI.activeSelf;
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
