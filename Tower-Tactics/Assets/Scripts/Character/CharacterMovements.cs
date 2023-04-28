using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    private float speed = 5.0f;
    private float movementThreshold = 0.01f;
    private float rotationSpeed = 720.0f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private Coroutine deathCoroutine;
    
    private bool isMoving;
    private bool isAttacking;
    private bool isDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementInput = new Vector3(horizontalInput, 0, verticalInput);

        // Temp
        isDead = Input.GetKey(KeyCode.LeftControl);
        isAttacking = Input.GetKey(KeyCode.Space) && !isDead;
        isMoving = movementInput.magnitude > movementThreshold && !isAttacking && !isDead;
        // Temp

        if (isMoving) {
            RotateTowardsMovementDirection();
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDead", isDead);  

        if (isDead)
        {
            deathCoroutine = StartCoroutine(ChangeCapsuleColliderHeight(0.9f, 2.3f));
            StartCoroutine(DestroyAfterDelay(3.0f));
        }
    }

    private void FixedUpdate()
    {
        if (rb != null && isMoving)
        {
            Vector3 movement = movementInput.normalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void RotateTowardsMovementDirection()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementInput);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeCapsuleColliderHeight(float targetHeight, float duration)
    {
        float initialHeight = capsuleCollider.height;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            capsuleCollider.height = Mathf.Lerp(initialHeight, targetHeight, t);
            yield return null;
        }

        capsuleCollider.height = targetHeight;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
