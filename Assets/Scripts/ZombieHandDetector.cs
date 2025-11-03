using Unity.VisualScripting;
using UnityEngine;

public class ZombieHandDetector : MonoBehaviour
{
    [Header("Settings for Hand Push")]
    [SerializeField]
    private GameObject objectToLift;
    [SerializeField]
    private float liftHeight = 1.5f;
    [SerializeField]
    private float liftSpeed = 1.0f;

    private bool shouldLift = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        if (objectToLift != null)
        {
            startPosition = objectToLift.transform.position;
            targetPosition = new Vector3(startPosition.x, startPosition.y + liftHeight, startPosition.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            shouldLift = true;
        }
    }

    private void Update()
    {
        if (shouldLift && objectToLift != null)
        {
            objectToLift.transform.position = Vector3.MoveTowards(
                objectToLift.transform.position,
                targetPosition,
                liftSpeed * Time.deltaTime
            );
            if (objectToLift.transform.position == targetPosition)
            {
                shouldLift = false;
            }
        }
    }
}
