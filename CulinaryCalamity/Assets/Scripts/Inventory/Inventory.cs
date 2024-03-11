using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Inventory
{
    public class PlayerInventory
    {

        public List<Tuple<Item, int>> InventoryContents { get; private set; }
        public int maxInvSize;

        public PlayerInventory()
        {
            InventoryContents = new List<Tuple<Item, int>>();
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void AddItem(Item item)
        {
            InventoryContents.Add(new Tuple<Item, int>(item, 1));
            foreach (Tuple<Item, int> it in InventoryContents)
            {
                Debug.Log(it);
            }

        }

    }
}