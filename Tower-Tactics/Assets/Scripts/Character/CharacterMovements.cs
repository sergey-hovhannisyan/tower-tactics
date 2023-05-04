using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovements : MonoBehaviour
{
    private float speed = 5.0f;
    private float movementThreshold = 0.01f;
    private float rotationSpeed = 720.0f;

    public NavMeshAgent agent;
    public Transform endpoint;
    private Rigidbody rb;
    private Vector3 movementInput;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private Coroutine deathCoroutine;

    
    public bool isMoving = true;
    public bool isAttacking = false;
    public bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(endpoint.position);

        if (agent.velocity.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDead", isDead);  

        if (isDead)
        {
            deathCoroutine = StartCoroutine(ChangeCapsuleColliderHeight(0.9f, 2.3f));
            StartCoroutine(DestroyAfterDelay(3.0f));
        }

        if (Vector3.Distance(transform.position, endpoint.position) < 0.5f){
            Destroy(gameObject);
        }
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