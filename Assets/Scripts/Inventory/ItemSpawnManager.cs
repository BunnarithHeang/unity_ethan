using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponPrefabs;
    [SerializeField] private List<GameObject> healthPrefabs;
    private List<Transform> spawnPoints = new List<Transform>();

    public List<InventoryItem> inventoryItemObjects { get; private set; }

    private float spawnTime = 100.0f;
    private float radius = 1.0f;

    private void Awake()
    {
        inventoryItemObjects = new List<InventoryItem>();

        
        List<GameObject> cp = FindGameObjectInChildWithTag(gameObject, "ItemCheckPoint");
        for (int i = 0; i < cp.Count; ++i)
        {
            spawnPoints.Add(cp[i].transform);
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnRandomItems", spawnTime, 30.0f);
       
        SpawnWeapons();

        for (int i = 0; i < 5; ++i)
        {
            SpawnRandomItems();
        }
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
        Vector3 spawnPos0 = spawnPoints[0].position + (Vector3)Random.insideUnitCircle * radius;
        Instantiate(weaponPrefabs[0], spawnPos0, spawnPoints[0].rotation);

        Vector3 spawnPos1 = spawnPoints[1].position + (Vector3)Random.insideUnitCircle * radius;
        Instantiate(weaponPrefabs[0], spawnPos1, spawnPoints[1].rotation);
    }

    private void SpawnRandomItems()
    {

        int posIndex = Random.Range(0, spawnPoints.Count);
        int itemIndex = Random.Range(0, healthPrefabs.Count);

        Debug.Log(spawnPoints[posIndex].position);

        Vector3 spawnPos = spawnPoints[posIndex].position + (Vector3)Random.insideUnitCircle * radius;
        Instantiate(healthPrefabs[itemIndex], spawnPos, spawnPoints[0].rotation);
    }

    public static List<GameObject> FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                objects.Add(t.GetChild(i).gameObject);
            }
        }

        return objects;
    }
}
