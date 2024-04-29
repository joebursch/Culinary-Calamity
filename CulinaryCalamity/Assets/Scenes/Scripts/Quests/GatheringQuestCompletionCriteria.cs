using System.Collections.Generic;
using Items;

namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionCriterion
    /// Represents the criterion for a gathering task
    /// </summary>
    public class GatheringQuestCompletionCriterion : QuestCompletionCriterion
    {
        private ItemId _itemToGather;
        private int _quantityToGather;

        /// <summary>
        /// Determines if the questOwner has satisfied the criterion by collecting the appropriate type and quantity of items
        /// The questOwner is assumed to be a Player object (which implements IQuestOwner)
        /// </summary>
        /// <param name="questOwner">IQuestOwner</param>
        /// <returns>bool, true if the questOwner has the correct type and number of items in their inventory</returns>
        public override bool IsSatisfied(IQuestOwner questOwner)
        {
            return ((Player)questOwner).QueryInventory(_itemToGather, _quantityToGather);
        }

        // <summary>
        /// Assigns values based on parameters based. Assumes parameter dictionary is well-formed with all required parameters
        /// </summary>
        /// <param name="parameters">Dictionary(string,string)</param>
        public override void CopyFromDescription(Dictionary<string, string> parameters)
        {
            _itemToGather = (ItemId)int.Parse(parameters["itemToGather"]);
            _quantityToGather = int.Parse(parameters["quantityToGather"]);
        }
    }
}