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


    [Header("Sound Settings")]
    [SerializeField]
    private AudioClip[] hitSounds;  // Ã‡ÒÒË‚ Á‚ÛÍÓ‚ Û‰‡‡
    private AudioSource audioSource;



    static float maxAnglePerGate = 2.24f;
    private float currentStrikeForceHorizontal;

    private void Start()
    {
        currentStrikeForceHorizontal = InitialStrikeForceHorizontal;
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(DelayedLaunch());
    }
    private void Launch()
    {
        float MaxAngle = maxAnglePerGate * angleGateQuantity;
        float CurAngle = Random.value * 2 * MaxAngle - MaxAngle;
        Vector3 forwardVector = Vector3.forward;
        Vector3 rightTurnedVector = RotateVector(forwardVector, CurAngle + 180) + new Vector3(0,1, 0)* StrikeForceVertical;
        rb.AddForce(rightTurnedVector * currentStrikeForceHorizontal, ForceMode.Impulse);

        PlayRandomHitSound();

        Debug.Log(currentStrikeForceHorizontal);
    }
    private void PlayRandomHitSound()
    {
        if (hitSounds != null && hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            AudioClip randomSound = hitSounds[randomIndex];
            audioSource.PlayOneShot(randomSound);
        }
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
