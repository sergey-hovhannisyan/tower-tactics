using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovements : MonoBehaviour
{
    private float speed = 5.0f;
    private float movementThreshold = 0.01f;
    private float rotationSpeed = 720.0f;
    private float remainingDistanceThreshold = 0.5f;
    public int livesCost = 1;
    public int money = 10;

    private GameManager gameManager;
    private ShopManager shopManager;
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

    public AudioClip deathSound;
    private AudioSource audioSource;
    private bool deathSoundPlayed = false;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        shopManager = GameObject.FindObjectOfType<ShopManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (agent.enabled && endpoint != null){
            NavMeshHit hit;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, agent.height * 2f, NavMesh.AllAreas)){

                GameObject bait = GameObject.FindWithTag("Bait");
                if (bait != null){
                    agent.SetDestination(bait.transform.position);
                }
                else{
                    agent.SetDestination(endpoint.position);
                }

                if (!agent.pathPending && agent.remainingDistance <= remainingDistanceThreshold){
                    if(bait!=null){
                        Destroy(bait);
                    }
                    else{
                        gameManager.subtractlives(livesCost);
                        Destroy(gameObject);
                    }
                }
            }
            else{
                Debug.LogWarning("Agent is not on a valid NavMesh.");
            }
        }
        
        if (agent.velocity.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDead", isDead);  

        if (isDead){
            if (agent != null){
                agent.enabled = false;
            }
            gameObject.layer = 10;
            if(!deathSoundPlayed){
                PlayDeathSound();
                deathSoundPlayed = true;
                shopManager.addMoney(money);
            }
            
            deathCoroutine = StartCoroutine(ChangeCapsuleColliderHeight(0.9f, 2.3f));
            StartCoroutine(DestroyAfterDelay(3.0f));
        }

        //makes the charater stop if no path found
        // if(IsAgentOnNavMesh()){
        //     if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid){
        //         agent.isStopped = true;
        //         isMoving = false;
        //         isAttacking = true;
        //         gameManager.pathIsClear = false;
        //     }
        //     else{
        //         gameManager.pathIsClear = true;
        //         agent.isStopped = false;
        //         isMoving = true;
        //         isAttacking = false;
        //     }
        // }
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
        //_waveManager.DestroyEenmy(gameObject);
    }

    private bool IsAgentOnNavMesh()
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(agent.transform.position, out hit, 1f, NavMesh.AllAreas);
    }

    void PlayDeathSound()
    {
        // Set the AudioClip and play it
        audioSource.clip = deathSound;
        audioSource.Play();
    }
}