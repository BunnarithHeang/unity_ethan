using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
