using UnityEngine;

public class SimpleMoveAnimation : MonoBehaviour
{
    [SerializeField]
    private bool isPhysical = false;

    [SerializeField]
    private float animOffset = 0f;

    [SerializeField]
    private float animSpeed = 1f;

    [SerializeField]
    private float jumpCoefficient = 1f;

    private Vector3 initialPos;
    [SerializeField]
    private Vector3 movementRange = new Vector3(0.2f, 0.5f, 0.2f);

    private void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        float t = Time.time * animSpeed + animOffset;

        float cycleTimeX = t * Mathf.PI;
        float easedX = movementRange.x * 0.5f * (0.5f - Mathf.Cos(cycleTimeX));

        float offsetX = Mathf.PingPong(t, movementRange.x);
        float offsetY;
        if (isPhysical)
        {
            offsetY = Mathf.Abs(Mathf.Sin(t * jumpCoefficient) * movementRange.y);
        }
        else
        {
            float cycleTimeY = t * Mathf.PI;
            offsetY = movementRange.y * 0.5f * (0.5f - Mathf.Cos(cycleTimeY));
        }
        float offsetZ = Mathf.PingPong(t, movementRange.z);
        float cycleTimeZ = t * Mathf.PI;
        float easedZ = movementRange.z * 0.5f * (0.5f - Mathf.Cos(cycleTimeZ));
        transform.position = initialPos + new Vector3(easedX, offsetY, easedZ);
    }
}
