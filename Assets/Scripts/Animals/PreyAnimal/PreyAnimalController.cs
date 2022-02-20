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
    float timer = 0;

    private NavMeshAgent agent;
    private Animator anim;
    public GameObject meatPrefab;
    //create HealthBarz object
    public HealthBarSpaceWorld healthbar;
    [SerializeField] private SpawnManager spawnManager;


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
        float limitedTime = Random.Range(5, 10);


        timer += Time.deltaTime;
        if (currentHealth > 0)
        {
            if (isNearPlayer)
            {

                // Move towards the player
                StartRunning();
                //cancle timer to 0
                timer = 0.0f;
            }
            else if (timer > 13.0f)
            {
                timer = 0;
                agent.SetDestination(transform.position);

            }

            else if (timer > limitedTime)
            {
                StartRunning();

            }
            else
            {

                agent.SetDestination(transform.position);
            }
        }
    }

    private void StartRunning()
    {
        
        Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
        agent.SetDestination(newPosition);

        anim.SetTrigger("isRunning");
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
            agent.SetDestination(transform.position);
            anim.SetTrigger("isDeath");

            StartCoroutine("SpawnItem");
        }
        
    }
    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);

        Instantiate(meatPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
