using UnityEngine;
using System.Collections;
public class BallLaunch : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float angleGateQuantity = 2;
    [SerializeField]
    private float StrikeForce = 15.0f;


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
        Vector3 rightTurnedVector = RotateVector(forwardVector, CurAngle + 180);
        rb.AddForce(rightTurnedVector * StrikeForce, ForceMode.Impulse);
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
