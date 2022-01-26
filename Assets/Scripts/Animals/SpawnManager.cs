using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Transform[] SpawnPoints;
    private float spawnTime = 10.0f;
    List<GameObject> limitedAnimals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("SpawnAnimals", spawnTime, spawnTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (limitedAnimals.Count > 10)
            CancelInvoke();

    }

    void SpawnAnimals()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        Vector3 spawnPos = SpawnPoints[spawnIndex].position;

        Instantiate(animalPrefabs[animalIndex], spawnPos, SpawnPoints[spawnIndex].rotation);
        limitedAnimals.Add(animalPrefabs[animalIndex]);

    }
}
