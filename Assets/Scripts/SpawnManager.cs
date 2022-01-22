using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Transform[] SpawnPoints;
    private float spawnTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimals", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAnimals()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        Vector3 spawnPos = SpawnPoints[spawnIndex].position;

        Instantiate(animalPrefabs[animalIndex], spawnPos, SpawnPoints[spawnIndex].rotation);
    }
}
