using UnityEngine;
using System.Collections;
public class BallLaunch : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float angleGateQuantity = 2;
    [SerializeField]
    private float StrikeForceHorizontal = 15.0f;
    [SerializeField]
    private float StrikeForceVertical = 0.2f;


    static float maxAnglePerGate = 2.24f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayedLaunch());
    }
    private void Launch()
    {
        float MaxAngle = maxAnglePerGate * angleGateQuantity;
        float CurAngle = Random.value * 2 * MaxAngle - MaxAngle;
        Vector3 forwardVector = Vector3.forward;
        Vector3 rightTurnedVector = RotateVector(forwardVector, CurAngle + 180) + new Vector3(0,1, 0)* StrikeForceVertical;
        rb.AddForce(rightTurnedVector * StrikeForceHorizontal, ForceMode.Impulse);
    }

    public IEnumerator DelayedLaunch(float delayTime = 3.0f)
    {
        yield return new WaitForSeconds(delayTime);
        Launch();
    }


    private Vector3 RotateVector(Vector3 inputVector, float angleDegrees)
    {
        Vector3 rotationAxis = Vector3.up;

        Quaternion rotation = Quaternion.AngleAxis(angleDegrees, rotationAxis);

        Vector3 rotatedVector = rotation * inputVector;

        return rotatedVector;
    }
}
