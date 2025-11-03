using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float movementRange = 3f; // �� ����������� X
    [SerializeField] private float movementHeight = 0.5f; // �� ��������� Y

    [Header("Gate Settings")]
    [SerializeField] private Transform gate;
    [SerializeField] private Vector3 offsetFromGate = new Vector3(0, 0, -1f);

    [Header("Difficulty")]
    [SerializeField] private float speedIncreasePerGoal = 0.5f; // �� ������� ����������� �������� �� ���
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
        FollowGateAndMoveSine();
    }

    void FollowGateAndMoveSine()
    {
        if (gate == null) return;


        Vector3 basePosition = gate.position + gate.TransformDirection(offsetFromGate);

        // ����: ���� ���� �������� 11, ��� ������� �������� ������ ����� x, ���� ����� � ��� �� ����� ��� ����� 0.001 ������� �������� � ������, �������� �������������, ��
        // �������� ���������� �� �� 0.001 ��� ������ ����, � �� x + a, ��� a - �����-�� ������� �����. ��� ��-�� ����, ��� �������� ���� �����������.
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
        }
        else
        {
        }
    }
    public void ResetSpeed()
    {
        currentSpeed = movementSpeed;
    }
}