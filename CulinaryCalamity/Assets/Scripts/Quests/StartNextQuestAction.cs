namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionAction
    /// This action will assign a new quest to a quest owner
    /// Used for linked quests that should be completed one after the other (ie: storyline quests)
    /// </summary>
    public class StartNextQuestAction : QuestCompletionAction
    {
        private Quest _nextQuest;
        private IQuestOwner _nextQuestOwner;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nextQuest">Quest, quest to be assigned</param>
        /// <param name="owner">IQuestOwner, object to assign quest to</param>
        public StartNextQuestAction(Quest nextQuest, IQuestOwner owner)
        {
            _nextQuest = nextQuest;
            _nextQuestOwner = owner;
        }

        /// <summary>
        /// Implementation of abstract method - represents taking the action
        /// Assigns the next quest to the specified owner
        /// </summary>
        public override void Take()
        {
            QuestFramework.GetQuestFramework().AssignQuest(_nextQuest, _nextQuestOwner);
        }
    }
}