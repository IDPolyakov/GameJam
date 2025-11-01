using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private InputActionReference move;

    [SerializeField]
    private InputActionReference jump;

    [SerializeField]
    private float jumpForce = 1.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        RigidbodyConstraints rotationLock = RigidbodyConstraints.FreezeRotation;
        RigidbodyConstraints positionLock = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rb.constraints = rotationLock | positionLock;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = move.action.ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = Time.deltaTime * moveSpeed;
        transform.position += moveVector.normalized * moveDistance;

        float jumpPressed = jump.action.ReadValue<float>();
    }

    private void OnEnable()
    {
        jump.action.Enable();
    }

    //private void OnDisable()
    //{
    //    jump.action.started -= Jump;
    //}

    void FixedUpdate()
    {
        if (jump.action.IsPressed() && transform.position.y < 0.1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("jumping!");
        }
    }
}
