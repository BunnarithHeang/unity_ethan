using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorController : MonoBehaviour
{
  
    private float lookRadius = 10.0f;
    private int MaxHealth = 100;
    private int currentHealth;

    private NavMeshAgent agent;
    private Animator anim;

    public HealthBar healthbar;

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

        if (distance <= lookRadius)
        {
            // Move towards the player
            Chasing(GameObject.FindGameObjectWithTag("Player").transform.position);
            anim.SetTrigger("isRunning");
        }

    }

    private void Chasing(Vector3 playerPosition)
    {
        
        agent.SetDestination(playerPosition);
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
