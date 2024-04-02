using Items;
using Inventory;

namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionAction
    /// Adds a quantity of one item type to an inventory
    /// </summary>
    public class AddItemAction : QuestCompletionAction
    {
        private ItemId _itemIdToAdd;
        private int _qtyToAdd;
        private PlayerInventory _inventory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemToAdd">ItemId</param>
        /// <param name="qty">int</param>
        /// <param name="inventory">PlayerInventory</param>
        public AddItemAction(ItemId itemToAdd, int qty, PlayerInventory inventory)
        {
            _itemIdToAdd = itemToAdd;
            _qtyToAdd = qty;
            _inventory = inventory;
        }

        /// <summary>
        /// Concrete implementation of abstract Take method
        /// Adds specified item in the specified quantity to the specified inventory
        /// </summary>
        public override void Take()
        {
            _inventory.AddItems(_itemIdToAdd, _qtyToAdd);
        }
    }
}