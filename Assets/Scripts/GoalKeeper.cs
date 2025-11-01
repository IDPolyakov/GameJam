using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 3f;
    public float movementRange = 3f; // по горизонтали X
    public float movementHeight = 0.5f; // по вертикали Y

    [Header("Gate Settings")]
    public Transform gate; // —сылка на ворота
    public Vector3 offsetFromGate = new Vector3(0, 0, -1f);

    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        FollowGateAndMoveSine();
    }

    void FollowGateAndMoveSine()
    {
        if (gate == null) return;

        
        Vector3 basePosition = gate.position + gate.TransformDirection(offsetFromGate);

        
        float sineX = Mathf.Sin((Time.time + randomOffset) * movementSpeed) * movementRange;
        float sineY = Mathf.Sin((Time.time + randomOffset) * movementSpeed * 1.5f) * movementHeight;

        Vector3 localMovement = new Vector3(sineX, sineY, 0);

        Vector3 worldMovement = gate.TransformDirection(localMovement);

      
        Vector3 targetPosition = basePosition + worldMovement;

        transform.position = targetPosition;

        transform.rotation = gate.rotation;
    }
}