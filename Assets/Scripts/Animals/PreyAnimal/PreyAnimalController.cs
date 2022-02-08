using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreyAnimalController : MonoBehaviour
{
    private float radius = 10;
    private float lookRadius = 3.0f;
    private int MaxHealth = 20;
    private int currentHealth;

    private NavMeshAgent agent;
    private Animator anim;

    public HealthBar healthbar;
    // denotes the schedule running animation
    private bool isAnimating = false;
    // prevents starting multiple identicle coroutine
    private bool hasRandomCoroutineStarted = false;
    // denotes the the animal had just ran after come close to the player
    private bool justRan = false;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = MaxHealth;
        healthbar.SetMaxHealth(MaxHealth);
    }


    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        bool isNearPlayer = distance <= lookRadius;

        float walkingTime = Random.Range(0, 500);

        if (isNearPlayer)
        {
            // if currently animating and player enters the zone
            if (isAnimating)
            {
                CancelRandomRunning();
            }

            // Move towards the player
            StartRunning();
            justRan = true;
        }
        else if (!hasRandomCoroutineStarted && walkingTime == 0 && !isAnimating && !justRan)
        {
            // if hasn't start the coroutine
            StartCoroutine("StartRandomRunning");
        } else if (isAnimating && !justRan)
        {
            // if currently animating and not triggering by the radius zone
            anim.SetTrigger("isRunning");
        }
        else if (justRan)
        {
            // justRan means the player enters the zone
            justRan = false;
            agent.SetDestination(transform.position);
        }
        
    }

    private void CancelRandomRunning() {
        isAnimating = false;
        hasRandomCoroutineStarted = false;

        StopCoroutine("StartRandomRunning");
    }

    private IEnumerator StartRandomRunning()
    {
        hasRandomCoroutineStarted = true;
        yield return new WaitForSeconds(1.5f);
        isAnimating = true;

        StartRunning();

        // Duration to wait for + buffer
        float dur = DurationFrom(agent.destination);
        
        yield return new WaitForSeconds(dur + 0.35f);
        
        isAnimating = false;
        hasRandomCoroutineStarted = false;
    }

    private float DurationFrom(Vector3 positon)
    {
        return Vector3.Distance(transform.position, positon) / agent.speed;
    }

    private void StartRunning()
    {
        Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
        agent.SetDestination(newPosition);
        
        anim.SetTrigger("isRunning");
    }

    void TakeDamage(int demage)
    {
        currentHealth -= demage;
        healthbar.SetHealth(currentHealth);
    }


    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(5);
      
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }



    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;


        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }
}
