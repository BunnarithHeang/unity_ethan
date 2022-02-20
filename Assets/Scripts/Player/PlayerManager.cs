using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inventoryParent;
    [SerializeField] private ItemSpawnManager itemSpawnManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private List<GameObject> spawningItems;
    [SerializeField] private GameObject weapon;
    private bool isWeapon = false;

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
        if (sender is InventoryItem)
        {
            switch ((sender as InventoryItem).type)
            {
                case InventoryItem.ItemType.Health:
                    healthBar.IncreaseHealth(15);
                    break;
                case InventoryItem.ItemType.Meat1:
                case InventoryItem.ItemType.Meat2:
                    healthBar.IncreaseHealth(10);
                    break;
            }
        }
    }

    private void OnDropItem(object sender, System.EventArgs e)
    {
        if (sender is InventoryItem)
        {
            InventoryItem.ItemType type = (sender as InventoryItem).type;

            int index = spawningItems.FindIndex(
                item => item.tag == InventoryItem.TagFromType(type));

            Vector3 position = player.transform.position + new Vector3(3, 0, 0);

            Instantiate(
                spawningItems[index],
                new Vector3(position.x, 0.7f, position.z),
                spawningItems[index].transform.rotation);
        }
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
        if (InventoryItem.IsInventoryItem(other.tag))
        {
            inventory.AddItemBy(other.tag);
            isWeapon = inventory.IsWeapon();
            print(isWeapon);
            weapon.SetActive(isWeapon);
            Destroy(other.gameObject);
        }
    }

    public void SetNewFocusIndex()
    {
        inventory.SetNewFocusIndex();

        isWeapon = inventory.IsWeapon();
        // check here for sword holding
        weapon.SetActive(isWeapon);
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