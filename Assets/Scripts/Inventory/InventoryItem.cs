using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public enum ItemType
    {
        Health, Sword, Batton
    }

    public ItemType itemType;
    public int amount;
}
