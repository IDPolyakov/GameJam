using UnityEngine;

public class PushBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Push Settings")]
    [SerializeField]
    private float pushForce = 5.0f;
    [SerializeField]
    private float upwardForce = 2.0f;



    [Header("Sound Settings")]
    [SerializeField]
    private AudioClip[] hitSounds;  // Массив звуков
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                Vector3 ballPosition = collision.transform.position;
                Vector3 spiderPosition = transform.position;

                float direction = ballPosition.x < spiderPosition.x ? -1f : 1f;

                Vector3 pushDirection = new Vector3(direction * pushForce, upwardForce, 0f);

                ballRigidbody.AddForce(pushDirection, ForceMode.Impulse);

                PlayRandomHitSound();
            }
        }
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


}
