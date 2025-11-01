using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlle : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) {
            inputVector.y = 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }

        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVector, moveDistance);
        if (!canMove)
        {
            // Attempt X movement
            moveVector = new Vector3(inputVector.x, 0,0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVector, moveDistance);
            if (!canMove)
            {
                // Attempt Z movement
                moveVector = new Vector3(0, 0, inputVector.y);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVector, moveDistance);
            }
        }
        if (canMove)
        {
            transform.position += moveVector.normalized * moveDistance;
        }
        moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
        float rotationSpeed = 10f;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveVector, Time.deltaTime * rotationSpeed);

        }
    }
}
