using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private List<Tuple<Item, int>> _inventoryContents;
    [SerializeField] private int _maxInvSize;

    public Inventory()
    {
        _inventoryContents = new List<Tuple<Item, int>>();
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">Item to be added</param>
    public void AddItem(Item item)
    {
        _inventoryContents.Add(new Tuple<Item, int>(item, 1));
    }

}