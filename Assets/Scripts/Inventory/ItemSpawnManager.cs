using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPrefabs;
    [SerializeField] private List<GameObject> healthPrefabs;
    [SerializeField] private Transform[] SpawnPoints;

    public List<InventoryItem> inventoryItemObjects { get; private set; }

    private float spawnTime = 5.0f;
    private float radius = 1.0f;

    private void Awake()
    {
        inventoryItemObjects = new List<InventoryItem>();
    }

    private void Start()
    {
        InvokeRepeating("SpawnAnimals", spawnTime, spawnTime);
        SpawnWeapons();
    }

    private InventoryItem.ItemType GetTypeFromTag(string tag)
    {
        switch (tag)
        {
            case "ItemPotionHealth":
                return InventoryItem.ItemType.Health;
            case "ItemSword":
                return InventoryItem.ItemType.Sword;
            default:
                return InventoryItem.ItemType.Health;
        }
    }

    void SpawnWeapons()
    {
        Vector3 spawnPos0 = SpawnPoints[0].position + (Vector3)Random.insideUnitCircle * radius;
        Instantiate(healthPrefabs[0], spawnPos0, SpawnPoints[0].rotation);

        Vector3 spawnPos1 = SpawnPoints[1].position + (Vector3)Random.insideUnitCircle * radius;
        Instantiate(healthPrefabs[0], spawnPos1, SpawnPoints[1].rotation);
    }

    void SpawnAnimals()
    {
        int index = Random.Range(0, healthPrefabs.Count);

        if (index == 0)
        {
            Vector3 spawnPos = SpawnPoints[0].position + (Vector3)Random.insideUnitCircle * radius;
            Instantiate(healthPrefabs[index], spawnPos, SpawnPoints[0].rotation);
        }
        else if (index == 1)
        {
            Vector3 spawnPos = SpawnPoints[1].position + (Vector3)Random.insideUnitCircle * radius;
            Instantiate(healthPrefabs[index], spawnPos, SpawnPoints[1].rotation);
        }
    }
}
