using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class HitBall : MonoBehaviour
{
    [SerializeField]
    private float animationDelay = 1.0f;
    [SerializeField]
    private Animator animator;

    private string animationName = "Scene";
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private bool isAnimating = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        playAnimation();
    }

    public void playAnimation()
    {
        if (!isAnimating)
        {
            ResetAnimation();
            StartCoroutine(PlayAnimationWithDelay(animationDelay));
        }
    }

    private IEnumerator PlayAnimationWithDelay(float delay)
    {
        isAnimating = true;
        yield return new WaitForSeconds(delay);
        if (animator != null)
        {
            Debug.Log("Animation Played");

            animator.Play(animationName, -1, 0.0f);

            isAnimating = false;
        }
    }
    public void ResetAnimation()
    {
        animator.Rebind();
        animator.Update(0.0f);
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}