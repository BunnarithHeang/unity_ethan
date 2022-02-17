using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private float rotationSpeed = 15.0f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, 6f * rotationSpeed * Time.deltaTime, 0));
    }
}
