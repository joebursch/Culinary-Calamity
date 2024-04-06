using Items;
using Inventory;
using System.Collections.Generic;

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
        /// Concrete implementation of abstract Take method
        /// Adds specified item in the specified quantity to the specified inventory
        /// </summary>
        public override void TakeAction()
        {
            _inventory.AddItems(_itemIdToAdd, _qtyToAdd);
        }

        /// <summary>
        /// Assigns values based on parameters based. Assumes parameter dictionary is well-formed with all required parameters
        /// </summary>
        /// <param name="parameters">Dictionary(string,string)</param>
        public override void CopyFromDescription(Dictionary<string, string> parameters)
        {
            _itemIdToAdd = (ItemId)int.Parse(parameters["itemIdToAdd"]);
            _qtyToAdd = int.Parse(parameters["qtyToAdd"]);
            string playerTag = parameters["playerHavingInventory"];
            _inventory = ((Player)QuestFramework.GetQuestFramework().GetQuestOwner(playerTag)).GetInventory();
        }
    }
}