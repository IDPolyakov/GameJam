using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float movementRange = 10.0f;

    private Rigidbody rb;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, movementRange * 2) - movementRange;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
