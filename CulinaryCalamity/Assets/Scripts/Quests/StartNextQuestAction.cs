namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionAction
    /// This action will assign a new quest to a quest owner
    /// Used for linked quests that should be completed one after the other (ie: storyline quests)
    /// </summary>
    public class StartNextQuestAction : QuestCompletionAction
    {
        private int _nextQuestId;
        private IQuestOwner _nextQuestOwner;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nextQuest">Quest, quest to be assigned</param>
        /// <param name="owner">IQuestOwner, object to assign quest to</param>
        public StartNextQuestAction(int nextQuestId, IQuestOwner owner)
        {
            _nextQuestId = nextQuestId;
            _nextQuestOwner = owner;
        }

        /// <summary>
        /// Implementation of abstract method - represents taking the action
        /// Assigns the next quest to the specified owner
        /// </summary>
        public override void Take()
        {
            QuestFramework.GetQuestFramework().AssignQuest(_nextQuestId, _nextQuestOwner);
        }
    }
}