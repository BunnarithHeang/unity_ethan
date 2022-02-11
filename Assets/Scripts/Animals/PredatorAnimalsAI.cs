using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Pursue, Attack, Die, Search
}

public class PredatorAnimalsAI : MonoBehaviour
{

    private float radius = 20;
    private float lookRadius = 3.0f;
    private int MaxHealth = 100;
    private int currentHealth;

    private NavMeshAgent agent;
    private Animator anim;
    //create HealthBarz object
    public HealthBar healthbar;



    private EnemyState currentState = EnemyState.Search;

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

        bool PlayerInAttackRange = distance <= lookRadius;
        
        switch (currentState)
        {
            case EnemyState.Search:
                Searching();
                if (PlayerInAttackRange)
                {
                    Pursue();
                    ChangeState(EnemyState.Attack);
                }
                break;
            case EnemyState.Pursue:
                if (currentHealth <= 0)
                {
                    ChangeState(EnemyState.Die);
                }
                // Players enter the attack state within the attack range.
                if (PlayerInAttackRange)
                {
                    Pursue();
                    ChangeState(EnemyState.Attack);
                }
                break;

            case EnemyState.Attack:

                if (currentHealth <= 0)
                {
                    ChangeState(EnemyState.Die);
                }
                if (!PlayerInAttackRange)
                {
                    ChangeState(EnemyState.Search);
                }
                break;
            
            case EnemyState.Die:
                Debug.Log("zombie death");
                break;
        }
    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }


    //private void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    anim = GetComponent<Animator>();
    //    currentHealth = MaxHealth;
    //    healthbar.SetMaxHealth(MaxHealth);
    //}


    //private void Update()
    //{
    //    float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

    //    bool isNearPlayer = distance <= lookRadius;


    //    timer += Time.deltaTime;

    //    if (isNearPlayer)
    //    {

    //        // Move towards the player
    //        Pursue();
    //        //cancle timer to 0
    //        timer = 0.0f;
    //    }
    //    else if (timer > 13.0f)
    //    {
    //        timer = 0;
    //        agent.SetDestination(transform.position);

    //    }

    //    else if (timer > 10.0f)
    //    {
    //        Pursue();

    //    }
    //    else
    //    {

    //        agent.SetDestination(transform.position);
    //    }
    //}

    private void Searching()
    {

        Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
        agent.SetDestination(newPosition);

        anim.SetFloat("Speed", 0.5f);
    }

    private void Pursue()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        anim.SetFloat("Speed", 1.0f);
    }


    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;


        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }

    void TakeDamage(int demage)
    {
        currentHealth -= demage;
        healthbar.SetHealth(currentHealth);
    }


    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(5);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


}

