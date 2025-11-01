using UnityEngine;
using UnityEngine.InputSystem;

public class ImpulseController : MonoBehaviour
{
    [SerializeField] public float impulseForce = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = -1;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = -1;
        }

        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * impulseForce;

        if (moveVector.x != 0 || moveVector.z != 0) {
            rb.AddForce(moveVector * moveDistance, ForceMode.Impulse);
        }
    }
}
