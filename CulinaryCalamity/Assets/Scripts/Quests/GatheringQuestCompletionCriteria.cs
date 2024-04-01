using Items

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
        public bool IsSatisfied(QuestOwner questOwner)
        {
            return ((Player)questOwner).CheckInventory(_itemToGather, _quantityToGather);
        }
    }
}