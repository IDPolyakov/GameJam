using System.Collections;
using UnityEngine;

public class ImpulseController : MonoBehaviour
{

    [Header("Impulse")]
    [SerializeField] private float impulseForce = 1f;

    [Header("UI")]
    [SerializeField] private UIManager linkToUIManager;

    [Header("Links to send \"Restart\" message")]
    [SerializeField] private GoalKeeper goalKeeper;
    [SerializeField] private SpawnEvents eventSpawner;
    [SerializeField] private HitBall hitBall;


    [Header("Sound Settings")]
    [SerializeField] private AudioClip crowdCheerSound;
    [SerializeField] private AudioClip crowdDisappointSound;
    [SerializeField] private AudioClip windSound;
    [SerializeField] private AudioClip crowdAmbientSound;
    private AudioSource audioSource;
    private AudioSource windAudioSource;  // Отдельный AudioSource для ветра
    private AudioSource crowdAmbientSource;


    [Header("Stop checker")]
    [SerializeField] private float VelocityStopThreshold = -0.05f; // Amount of speed, below which real speed is considered zero or negative
    [SerializeField] private float BackwardDuration = 3.0f; // Time of necessary backwards movement

    private float backwardTimer = 0f;

    private Rigidbody rb;
    private BallLaunch launcher;
    private Vector3 initialCords;

    private void Start()
    {
        initialCords = transform.position;
        rb = GetComponent<Rigidbody>();
        launcher = GetComponent<BallLaunch>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        windAudioSource = gameObject.AddComponent<AudioSource>();
        SetupWindSound();

        crowdAmbientSource = gameObject.AddComponent<AudioSource>();
        SetupCrowdAmbientSound();


        if (hitBall == null)
        {
            hitBall = FindFirstObjectByType<HitBall>();
        }
    }
    private void SetupWindSound()
    {
        if (windSound != null && windAudioSource != null)
        {
            windAudioSource.clip = windSound;
            windAudioSource.loop = true; 
            windAudioSource.volume = 0.6f; 
            windAudioSource.playOnAwake = true;
            windAudioSource.Play();
        }
    }

    private void SetupCrowdAmbientSound()
    {
        if (crowdAmbientSound != null && crowdAmbientSource != null)
        {
            crowdAmbientSource.loop = true;
            crowdAmbientSource.volume = 0.8f;
            crowdAmbientSource.clip = crowdAmbientSound;
            crowdAmbientSource.playOnAwake = true;
            crowdAmbientSource.Play();
        }
    }

    private void Update()
    {
        if (!isRestarting && (transform.position.z < -10 || transform.position.z > 90))
        {
            OnGameOver();
        }


        //arrowMovement();
        checkForForwardMovement();
    }

    // �������� � �������
    private void OnTriggerEnter(Collider other)
    {

        if (!isRestarting && other.CompareTag("Goal"))
        {

            // ��������� ����
            PlayCrowdCheer();
            linkToUIManager.UpdateScore();
            goalKeeper.IncreaseSpeed();

            // �������
            if (!isRestarting)
            {
                StartCoroutine(DelayedRestart(1.0f));
            }
        }
    }

    private void PlayCrowdCheer()
    {
        if (crowdCheerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(crowdCheerSound);
        }
    }
    private void PlayCrowdDisappoint()
    {
        if (crowdDisappointSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(crowdDisappointSound);
        }
    }
    private void checkForForwardMovement()
    {
        float currentZVelocity = rb.linearVelocity.z;
        if (currentZVelocity > VelocityStopThreshold)
        {
            backwardTimer += Time.deltaTime;
        }
        else
        {
            backwardTimer = 0f;
        }

        if (!isRestarting && backwardTimer >= BackwardDuration)
        {
            OnGameOver();
        }
    }

    private void restorePosition()
    {
        transform.position = initialCords;
        transform.rotation = new Quaternion();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        eventSpawner.respawnPrefabs();
        hitBall.playAnimation();
    }

    private bool isRestarting = false;
    public IEnumerator DelayedRestart(float delayTime)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delayTime);
        restorePosition();
        isRestarting = false;
        linkToUIManager.StartCountdown();
        StartCoroutine(launcher.DelayedLaunch());
    }

    private void OnGameOver()
    {
        PlayCrowdDisappoint();
        launcher.resetForce();
        goalKeeper.ResetSpeed();
        linkToUIManager.ShowGameOver();
        eventSpawner.resetAmount();
        StartCoroutine(DelayedRestart(3.0f));
    }

    private void arrowMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = -1;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = -1;
        }

        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = Time.deltaTime * impulseForce;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            rb.AddForce(moveVector * moveDistance, ForceMode.Impulse);
        }
    }
}