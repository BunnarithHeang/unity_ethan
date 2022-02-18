using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField] private GameObject inventoryParent;
    [SerializeField] private ItemSpawnManager itemSpawnManager;
    [SerializeField] public HealthBar healthBar;

    private void Awake()
    {
        List<GameObject> objects = FindGameObjectInChildWithTag(
            inventoryParent,
            "InventoryItemSlot");

        inventory = new Inventory(objects);

        inventory.OnUseItem += OnUseItem;
        inventory.OnDropItem += OnDropItem;
    }

    private void OnUseItem(object sender, System.EventArgs e)
    {
        
    }

    private void OnDropItem(object sender, System.EventArgs e)
    {

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

    private void OnTriggerEnter(Collider other)
    {
        // Filters for only inventory items
        if (InventoryItem.IsInventoryItem(other.tag))
        {
            inventory.AddItemBy(other.tag);
            Destroy(other.gameObject);
        }
    }

    public void SetNewFocusIndex()
    {
        inventory.SetNewFocusIndex();
    }

    public void UseItem()
    {
        inventory.UseItem();
    }

    public void DropItem()
    {
        inventory.DropItem();
    }
}