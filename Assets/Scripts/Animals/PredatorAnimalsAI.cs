//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;


//public class PredatorAnimalsAI : MonoBehaviour
//{
//    private NavMeshAgent agent;
//    private Animator anim;
//    public float startWaitTime = 4;                 
//    public float timeToRotate = 2;                  
//    public float speedWalk = 5;                     
//    public float speedRun = 9;

//    public float viewRadius = 15;                   //  Radius of the enemy view
//    public float viewAngle = 90;                    //  Angle of the enemy view


//    public Transform[] waypoints;
//    int m_CurrentWaypointIndex;
//    Vector3 playerLastPosition = Vector3.zero;
//    Vector3 wayPointTraget;
//    Transform target;


//    float m_WaitTime;                               //  Variable of the wait time that makes the delay
//    float m_TimeToRotate;                           //  Variable of the wait time to rotate when the player is near that makes the delay
//    float distance;
//    bool m_playerInRange;                           //  If the player is in range of vision, state of chasing
//    bool m_PlayerNear;                              //  If the player is near, state of hearing
//    bool m_IsPatrol;                                //  If the enemy is patrol, state of patroling
//    bool m_CaughtPlayer;

//    void Start()
//    {
//        //target = GameObject.FindGameObjectWithTag("Player").transform;
        
//        m_IsPatrol = true;
//        m_CaughtPlayer = false;
//        m_playerInRange = false;
//        m_PlayerNear = false;
//        m_WaitTime = startWaitTime;                 //  Set the wait time variable that will change
//        m_TimeToRotate = timeToRotate;

//                        //  Set the initial waypoint
//        agent = GetComponent<NavMeshAgent>();

//        agent.isStopped = false;
//        agent.speed = speedWalk;             //  Set the navemesh speed with the normal speed of the enemy
//        agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the destination to the first waypoint
//        UpdateDestination();
//    }

//    private void Update()
//    {
//        target = GameObject.FindGameObjectWithTag("Player").transform;
//        distance = Vector3.Distance(target.position, transform.position);
//        EnviromentView();                       //  Check whether or not the player is in the enemy's field of vision

//        if (!m_IsPatrol)
//        {
//            Chasing();
//        }
//        else
//        {
//            Patroling();
//        }
//    }


//    private void Patroling()
//    {
//        Debug.Log(Vector3.Distance(transform.position, wayPointTraget) < 1);
//        if (Vector3.Distance(transform.position, wayPointTraget) < 1)
//        {
           
            
//            if (m_WaitTime <= 0)
//            {
//                IterateWaypointIndex();
//                UpdateDestination();
//                Move(speedWalk);
                
//                m_WaitTime = startWaitTime;
//            }
//            else
//            {
//                Stop();
//                m_WaitTime -= Time.deltaTime;
//            }

//        }

//    }

//    void LookingPlayer(Vector3 player)
//    {
//        agent.SetDestination(player);
//        if (Vector3.Distance(transform.position, player) <= 0.3)
//        {
//            if (m_WaitTime <= 0)
//            {
//                m_PlayerNear = false;
//                Move(speedWalk);
//                agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
//                m_WaitTime = startWaitTime;
//                m_TimeToRotate = timeToRotate;
//            }
//            else
//            {
//                Stop();
//                m_WaitTime -= Time.deltaTime;
//            }
//        }
//    }

//    private void UpdateDestination()
//    {

//        wayPointTraget = waypoints[m_CurrentWaypointIndex].position;
//        agent.SetDestination(wayPointTraget);
//    }
//    private void IterateWaypointIndex()
//    {
//        m_CurrentWaypointIndex++;
//        if(m_CurrentWaypointIndex == waypoints.Length)
//        {
//            m_CurrentWaypointIndex = 0;
//        }
//    }
    

//    private void Chasing()
//    {
//        //  The enemy is chasing the player
//        m_PlayerNear = false;                       //  Set false that hte player is near beacause the enemy already sees the player
//        playerLastPosition = Vector3.zero;          //  Reset the player near position

//        if (m_playerInRange)
//        {
//            Move(speedRun);
           
//            agent.SetDestination(target.position);          //  set the destination of the enemy to the player location
//        }
        
//    }

//    void Move(float speed)
//    {
//        agent.isStopped = false;
//        agent.speed = speed;
//    }

//    void Stop()
//    {
//        agent.isStopped = true;
//        agent.speed = 0;
//    }

//    void EnviromentView()
//    {
//        //  Make an overlap sphere around the enemy to detect the playermask in the view radius
        

//        bool isNearPlayer = distance <= 15;
//        if (isNearPlayer)
//        {
//            m_playerInRange = true;
//            m_IsPatrol = false;

//        }
//        else
//        {
//            /*
//             *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
//             *  Or the enemy is a safe zone, the enemy will no chase
//             * */
//            m_playerInRange = false;                //  Change the sate of chasing
//        }
        
//    }

//}


////private float lookRadius = 13.0f;
////Transform target;
////private float radius = 20;
////private float timer = 0;

////States
////public float sightRange, attackRange;
////public bool playerInSightRange, playerInAttackRange;

////private void Start()
////{
////    agent = GetComponent<NavMeshAgent>();
////    anim = GetComponent<Animator>();
////    target = GameObject.FindGameObjectWithTag("Player").transform;
////    //currentHealth = MaxHealth;
////    //healthbar.SetMaxHealth(MaxHealth);
////}

////private void Update()
////{
////    timer += Time.deltaTime;


////    //Check for sight and attack range

////    float distance = Vector3.Distance(target.position, transform.position);

////    bool isNearPlayer = distance <= lookRadius;
////    float limitedTime = Random.Range(5, 10);

////    playerInSightRange = distance <= lookRadius;
////    playerInAttackRange = distance < agent.stoppingDistance;
////    //Debug.Log(!playerInSightRange && !playerInAttackRange);


////    if (timer > limitedTime)
////    {
////        Patrol();
////        timer = 0;

////    }







////    if (playerInSightRange && !playerInAttackRange && !alreadyAttacked)
////    {
////        ChasePlayer();
////        timer = 0.0f;
////    }
////    if (playerInAttackRange && playerInSightRange) AttackPlayer();
////}


////private void Patrol()
////{

////    Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
////    agent.SetDestination(newPosition);

////    //walk animation
////    anim.SetFloat("Speed", 0.5f);
////}


////public Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
////{
////    Vector3 randomDirection = Random.insideUnitSphere * distance;
////    randomDirection += origin;


////    NavMeshHit navHit;
////    NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

////    return navHit.position;
////}



////private void ChasePlayer()
////{
////    Debug.Log("Chassing");
////    agent.SetDestination(target.position);
////    anim.SetFloat("Speed", 1.0f);
////}


////private void ResetAttack()
////{
////    alreadyAttacked = false;
////}
////void FaceTarget()
////{
////    Vector3 direction = (target.position - transform.position).normalized;
////    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
////    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
////}

////private void AttackPlayer()
////{

////    FaceTarget();

////    if (!alreadyAttacked)
////    {
////        ///Attack code here
////        anim.SetTrigger("isAttacking");

////        alreadyAttacked = true;
////        Invoke("ResetAttack", timeBetweenAttacks * Time.deltaTime);
////    }
////    Debug.Log("Attacking Player");
////}




