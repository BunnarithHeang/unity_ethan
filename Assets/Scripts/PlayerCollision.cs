using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision entered");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered");
    }
}
