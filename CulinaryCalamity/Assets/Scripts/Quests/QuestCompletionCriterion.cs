namespace Quests
{
    /// <summary>
    /// Abstract class representing a criteria/task to be satisfied for quest completion.
    /// </summary>
    public abstract class QuestCompletionCriterion
    {
        /// <summary>
        /// Returns true if the criteria is satisified, false if it is not satisified.
        /// </summary>
        /// <returns>bool</returns>
        public abstract bool IsSatisfied(IQuestOwner questOwner);
    }
}