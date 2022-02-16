using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform tran;

    void LateUpdate()
    {
        transform.LookAt(transform.position + tran.forward);
    }
}
