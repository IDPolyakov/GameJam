using UnityEngine;
using System.Collections;

public class ImpulseController : MonoBehaviour
{

    [Header("Impulse")]
    [SerializeField] private float impulseForce = 1f;

    [Header("UI")]
    [SerializeField] private UIManager linkToUIManager;

    [Header("Stop checker")]
    [SerializeField] private float VelocityStopThreshold = 0.05f; // Amount of speed, below which real speed is considered zero or negative
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
    }

    private void Update()
    {
        
        if (!isRestarting && transform.position.z < -10) // ·˚ÎÓ < 0 
        {
            Debug.Log("ÃËÏÓ!");
            isRestarting = true;
            StartCoroutine(DelayedRestart(1.0f));  // œ≈–≈–«¿œ”— 
        }

        arrowMovement();
        checkForForwardMovement();
    }


    // “Ë„„Â ‚ ‚ÓÓÚ‡ı
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Goal"))
        {
            Debug.Log("√ŒÀ! +1 Ó˜ÍÓ");

            linkToUIManager.UpdateScore(); // ƒÓ·‡‚ÎˇÂÏ Ó˜ÍÓ

            // –ÂÒÚ‡Ú
            if (!isRestarting)
            {
                isRestarting = true;
                StartCoroutine(DelayedRestart(1.0f));
            }
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

        if (backwardTimer >= BackwardDuration)
        {
            ShowGameOver();
        }
    }

    private void restorePosition()
    {
        transform.position = initialCords;
        transform.rotation = new Quaternion();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private bool isRestarting = false;
    public IEnumerator DelayedRestart(float delayTime)
    {
        isRestarting = true;
        yield return new WaitForSeconds(delayTime);
        restorePosition();
        isRestarting = false;
        //linkToUIManager.UpdateScore(); // ƒŒ¡¿¬À≈Õ»≈ Œ◊ ¿ (·˚ÎÓ)
        linkToUIManager.StartCountdown();
        StartCoroutine(launcher.DelayedLaunch());
    }

    public void ShowGameOver()
    {
        linkToUIManager.ShowGameOver();
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
