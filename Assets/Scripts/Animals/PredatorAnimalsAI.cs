//using UnityEngine;
//using System.Collections;
//using UnityEngine.AI;

///// <summary>
///// Creates wandering behaviour for a CharacterController.
///// </summary>
//[RequireComponent(typeof(CharacterController))]
//public class PredatorAnimalsAI : MonoBehaviour
//{
//	public float speed = 0.5f;
//	public float directionChangeInterval = 1;
//	public float maxHeadingChange = 30;
//	private Animator anim;
//	private NavMeshAgent agent;

//	CharacterController controller;
//	float heading;
//	Vector3 targetRotation;

//	void Awake()
//	{
//		controller = GetComponent<CharacterController>();
//		anim = GetComponent<Animator>();
//		agent = GetComponent<NavMeshAgent>();

//		// Set random initial rotation
//		heading = Random.Range(0, 360);
//		transform.eulerAngles = new Vector3(0, heading, 0);

//		StartCoroutine(NewHeading());
//	}

//	void Update()
//	{
//		transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
//		var forward = transform.TransformDirection(Vector3.forward);
//		Vector3 newPosition = RandomNavSphere(transform.position, 10, -1);

//		controller.SimpleMove(newPosition * speed * Time.deltaTime);
//        anim.SetFloat("Speed", 0.5f);
//	}

//	/// <summary>
//	/// Repeatedly calculates a new direction to move towards.
//	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
//	/// </summary>
//	IEnumerator NewHeading()
//	{
//		while (true)
//		{
//			NewHeadingRoutine();

//			yield return new WaitForSeconds(directionChangeInterval);
//		}
//	}

//	/// <summary>
//	/// Calculates a new direction to move towards.
//	/// </summary>
//	void NewHeadingRoutine()
//	{
//		var floor = transform.eulerAngles.y - maxHeadingChange;
//		var ceil = transform.eulerAngles.y + maxHeadingChange;
//		heading = Random.Range(floor, ceil);
//		targetRotation = new Vector3(0, heading, 0);

//    }
//	public Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
//    {
//        Vector3 randomDirection = Random.insideUnitSphere * distance;
//        randomDirection += origin;


//        NavMeshHit navHit;
//        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

//        return navHit.position;
//    }

//}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorAnimalsAI : MonoBehaviour
{


    private float radius = 1;

    private NavMeshAgent agent;
    private Animator anim;
    private IEnumerator coroutine;
    private bool isWalking = false;
    private bool isWandering = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }


    private void Update()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        if (isWalking)
        {
            Walking();
            anim.SetFloat("Speed", 0.5f);
        }
    }


    //private IEnumerator StartAnimation(float waitingTime)
    //{
    //    while (true)
    //    {
    //        //anim.SetFloat("Speed", 0.0f);
    //        yield return new WaitForSeconds(waitingTime);
    //        AnimatePosition();
    //        yield return new WaitForSeconds(waitingTime);

    //    }

    //}
    private void Walking()
    {
        Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
        agent.SetDestination(newPosition);
    }

    private IEnumerator Wander()
    {
        int walkWait = Random.Range(5, 10);
        int walkTime = Random.Range(5, 10);
        //int rotateTime = Random.Range(1, 3);
        //int rotateWait = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        anim.SetFloat("Speed", 0.0f);
        isWandering = false;
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

