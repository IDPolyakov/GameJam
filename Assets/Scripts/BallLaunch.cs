using UnityEngine;
using System.Collections;
public class BallLaunch : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float angleGateQuantity = 2;
    [SerializeField]
    private float InitialStrikeForceHorizontal = 15.0f;
    [SerializeField]
    private float StrikeForceVertical = 0.2f;
    [SerializeField]
    private float StrikeForceHorizontal—hange = 2.0f;

    static float maxAnglePerGate = 2.24f;
    private float currentStrikeForceHorizontal;

    private void Start()
    {
        currentStrikeForceHorizontal = InitialStrikeForceHorizontal;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DelayedLaunch());
    }
    private void Launch()
    {
        float MaxAngle = maxAnglePerGate * angleGateQuantity;
        float CurAngle = Random.value * 2 * MaxAngle - MaxAngle;
        Vector3 forwardVector = Vector3.forward;
        Vector3 rightTurnedVector = RotateVector(forwardVector, CurAngle + 180) + new Vector3(0,1, 0)* StrikeForceVertical;
        rb.AddForce(rightTurnedVector * currentStrikeForceHorizontal, ForceMode.Impulse);
        Debug.Log(currentStrikeForceHorizontal);
    }

    public IEnumerator DelayedLaunch(float delayTime = 3.0f)
    {
        currentStrikeForceHorizontal += StrikeForceHorizontal—hange;
        yield return new WaitForSeconds(delayTime);
        Launch();
    }

    public void resetForce()
    {
        currentStrikeForceHorizontal = InitialStrikeForceHorizontal - StrikeForceHorizontal—hange;
    }


    private Vector3 RotateVector(Vector3 inputVector, float angleDegrees)
    {
        Vector3 rotationAxis = Vector3.up;

        Quaternion rotation = Quaternion.AngleAxis(angleDegrees, rotationAxis);

        Vector3 rotatedVector = rotation * inputVector;

        return rotatedVector;
    }
}
