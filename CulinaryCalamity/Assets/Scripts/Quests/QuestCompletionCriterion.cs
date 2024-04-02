using System.Collections.Generic;

namespace Quests
{
    /// <summary>
    /// Abstract class representing a criteria/task to be satisfied for quest completion.
    /// </summary>
    public abstract class QuestCompletionCriterion
    {
        /// <summary>
        /// Returns true if the criteria is satisified by the quest owner, false if it is not satisified.
        /// </summary>
        /// <returns>bool</returns>
        public abstract bool IsSatisfied(IQuestOwner questOwner);

        /// <summary>
        /// Populates an existing Quest Completion Criterion with values from a dictionary
        /// </summary>
        /// <param name="actionDescription">Dictionary(string, string) containing quest criterion parameters </param>
        public abstract void CopyFromDescription(Dictionary<string, string> parameters);

    }
}