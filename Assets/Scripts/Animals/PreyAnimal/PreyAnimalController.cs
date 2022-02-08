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

    private Vector3 lastestDirectionPosition = Vector3.zero;
    private bool isStopping = false;

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
        float walkingTime = Random.Range(0, 1000);

        if (distance <= lookRadius)
        {
            // Move towards the player
            Running();
            anim.SetTrigger("isRunning");
        }
        //else if (lastestDirectionPosition != Vector3.zero && !isStopping)
        //{
        //    isStopping = true;
        //    float dur = durationFromVector3(lastestDirectionPosition);
        //    StartCoroutine("StopAnimateAtDuration", dur);
        //}
        //else if (!isStopping)
        //{
        //    agent.SetDestination(transform.position);
        //}  else
        {
            lastestDirectionPosition = RandomNavSphere(transform.position, radius, -1);
        }
    }

    //private float durationFromVector3(Vector3 positon)
    //{
    //    return Vector3.Distance(transform.position, positon) / agent.speed;
    //}

    private IEnumerator setRunningTime(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        agent.SetDestination(transform.position);

        isStopping = false;
        lastestDirectionPosition = Vector3.zero;
    }

    private void Running()
    {
        Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
        var forward = transform.TransformDirection(Vector3.forward);
        agent.SetDestination(newPosition);

        lastestDirectionPosition = newPosition;
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
