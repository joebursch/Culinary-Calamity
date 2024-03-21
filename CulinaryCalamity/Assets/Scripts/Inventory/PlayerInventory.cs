using Items;
using System.Collections.Generic;

namespace Inventory
{
    /// <summary>
    /// Represents the players inventory - what items they have and how many
    /// </summary>
    public class PlayerInventory
    {

        public Dictionary<ItemId, int> InventoryContents { get; private set; } // ItemId -> quantity stored
        public int maxInvSize;

        public PlayerInventory()
        {
            InventoryContents = new();
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

    }
}