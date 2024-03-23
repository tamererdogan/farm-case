using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private PlayerInput playerInput;

    private Rigidbody rigidBody;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.Player.Move.Enable();
        playerInput.Player.Look.Enable();
    }

    void FixedUpdate()
    {
        Vector2 direction = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 movementVector = transform.forward * direction.y + transform.right * direction.x;
        rigidBody.velocity = movementVector * moveSpeed;

        Vector2 mouseDelta = playerInput.Player.Look.ReadValue<Vector2>();
        transform.rotation *= Quaternion.Euler(0, mouseDelta.x, 0);

        float desiredAngle = -mouseDelta.y;
        float nextAngle = Camera.main.transform.eulerAngles.x + desiredAngle;
        if (nextAngle > 180)
            nextAngle -= 360;

        if (nextAngle > 30 || nextAngle < -30)
            return;

        Camera.main.transform.rotation *= Quaternion.Euler(desiredAngle, 0, 0);
    }
}
