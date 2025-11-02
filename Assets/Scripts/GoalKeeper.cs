using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float movementRange = 3f; // по горизонтали X
    [SerializeField] private float movementHeight = 0.5f; // по вертикали Y

    [Header("Gate Settings")]
    [SerializeField] private Transform gate;
    [SerializeField] private Vector3 offsetFromGate = new Vector3(0, 0, -1f);

    [Header("Difficulty")]
    [SerializeField] private float speedIncreasePerGoal = 0.5f; // Ќа сколько увеличивать скорость за гол
    [SerializeField] private float maxSpeed = 8f;

    private float randomOffset;
    private float currentSpeed;

    void Start()
    {
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
        currentSpeed = movementSpeed;
        RigidbodyConstraints rotationLock = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().constraints = rotationLock;
    }

    void Update()
    {
        Debug.Log($"—корость вратар€: {currentSpeed}");
        FollowGateAndMoveSine();
    }

    void FollowGateAndMoveSine()
    {
        if (gate == null) return;

        
        Vector3 basePosition = gate.position + gate.TransformDirection(offsetFromGate);

        // Ѕага: если есть скорость 11, наш текущий аргумент синуса будет x, если игрок в это же врем€ или через 0.001 секунды попадает в ворота, скорость увеличиваетс€, но
        // аргумент измен€етс€ не на 0.001 как должно быть, а на x + a, где a - какое-то большое число. Ёто из-за того, что скорость есть коэффициент.
        float sineX = Mathf.Sin((Time.time + randomOffset) * currentSpeed) * movementRange;
        float sineZ = Mathf.Sin((Time.time + randomOffset) * currentSpeed * 1.5f) * movementHeight;

        Vector3 localMovement = new Vector3(sineX * 1.5f, 0, Mathf.Abs(sineZ * 6.0f));

        Vector3 worldMovement = gate.TransformDirection(localMovement);

      
        Vector3 targetPosition = basePosition + worldMovement;

        transform.position = targetPosition;

    }
    public void IncreaseSpeed()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreasePerGoal;
            Debug.Log($"—корость вратар€ увеличена до: {currentSpeed}");
        }
        else
        {
            Debug.Log("ƒостигнута максимальна€ скорость вратар€!");
        }
    }
    public void ResetSpeed()
    {
        currentSpeed = movementSpeed;
        Debug.Log("—корость вратар€ сброшена");
    }
}