using Items;

namespace Quests
{

    public class GatheringQuestCompletionCriterion : QuestCompletionCriterion
    {
        private ItemId _itemToGather;
        private int _quantityToGather;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questOwner"></param>
        /// <returns></returns>
        public override bool IsSatisfied(IQuestOwner questOwner)
        {
            return ((Player)questOwner).QueryInventory(_itemToGather, _quantityToGather);
        }
    }
}