using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Inventory
{
    public class PlayerInventory
    {

        public List<Tuple<ItemId, int>> InventoryContents { get; private set; }
        public int maxInvSize;

        public PlayerInventory()
        {
            InventoryContents = new List<Tuple<ItemId, int>>();
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void AddItem(ItemId itemId)
        {
            InventoryContents.Add(new Tuple<ItemId, int>(itemId, 1));
            Debug.Log(InventoryContents);
        }

    }
}