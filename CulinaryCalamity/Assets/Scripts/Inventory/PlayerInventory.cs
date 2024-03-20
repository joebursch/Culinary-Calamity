using Items;
using Saving;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Represents the players inventory - what items they have and how many
    /// </summary>
    public class PlayerInventory
    {

        public Dictionary<ItemId, int> InventoryContents { get; private set; } // ItemId -> quantity stored
        public int maxInvSize;
        private ObjectSaveData _inventorySaveData;


        /// <summary>
        /// Constructor. Initializes empty contents and ObjectSaveData.
        /// </summary>
        public PlayerInventory()
        {
            InventoryContents = new();
            _inventorySaveData = new();
            try
            {
                GameSaveManager.GetGameSaveManager().Save += OnSave;
                GameSaveManager.GetGameSaveManager().Load += OnLoad;
            }
            catch (NullReferenceException)
            {
                Debug.Log("No Game Save Manager Found");
            }
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemId">Item to be added</param>
        public void AddItem(ItemId itemId)
        {
            try
            {
                InventoryContents[itemId] += 1;
            }
            catch (KeyNotFoundException)
            {
                InventoryContents[itemId] = 1;
            }
        }

        /// <summary>
        /// Adds multiple of the same item to the inventory
        /// </summary>
        /// <param name="itemId">Item to be added</param>
        /// /// <param name="qty">how many copies to add</param>
        public void AddItems(ItemId itemId, int qty)
        {
            try
            {
                InventoryContents[itemId] += qty;
            }
            catch (KeyNotFoundException)
            {
                InventoryContents[itemId] = qty;
            }
        }

        /// <summary>
        /// Listener for the Save event. Pushes current state of the inventory to the GameSaveManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
            Dictionary<string, string> inventoryData = new() { };
            foreach (ItemId key in InventoryContents.Keys)
            {
                inventoryData.Add(key.ToString(), InventoryContents[key].ToString());
            };

            _inventorySaveData.UpdateSaveData(inventoryData);
            GameSaveManager.GetGameSaveManager().UpdateObjectSaveData("PlayerInventory", _inventorySaveData);
        }

        /// <summary>
        /// Listener for the load event. Pulls save data from GameSaveManager and performs appropriate updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnLoad(object sender, EventArgs e)
        {
            _inventorySaveData = GameSaveManager.GetGameSaveManager().GetObjectSaveData("PlayerInventory");
            foreach (string key in _inventorySaveData.SaveData.Keys)
            {
                ItemId itemId = (ItemId)int.Parse(key);
                int quantity = int.Parse(_inventorySaveData.SaveData[key]);
                InventoryContents[itemId] = quantity;
            }
        }

    }
}