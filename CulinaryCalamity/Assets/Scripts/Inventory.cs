using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private List<Item> _inventoryContents;
    [SerializeField] private int _maxInvSize;

    public Inventory()
    {
        _inventoryContents = new List<Item>();
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">Item to be added</param>
    public void AddItem(Item item)
    {
        Debug.Log("Adding item to inventory: " + item.GetName());
        _inventoryContents.Add(item);
    }

}