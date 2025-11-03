using UnityEngine;

public class PushBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Push Settings")]
    [SerializeField]
    private float pushForce = 5.0f;
    [SerializeField]
    private float upwardForce = 2.0f;

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
            }
        }
    }

}
