using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableItems;

    public List<InventoryItem> inventoryItemObjects { get; private set;}

    private void Awake()
    {
        inventoryItemObjects = new List<InventoryItem>();

        //for (int i = 0; i < spawnableItems.Count; ++i)
        //{
        //    GameObject gb = spawnableItems[i];

        //    InventoryItem inv = new InventoryItem();
        //    inv.type = GetTypeFromTag(gb.tag);
        //    inv.amount = 1;

        //    inventoryItemObjects.Add(inv);
        //}
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
}
