using System.Collections.Generic;

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
        /// Implementation of abstract method - represents taking the action
        /// Assigns the next quest to the specified owner
        /// </summary>
        public override void Take()
        {
            QuestFramework.GetQuestFramework().AssignQuest(_nextQuestId, _nextQuestOwner);
        }

        /// <summary>
        /// Assigns values based on parameters based. Assumes parameter dictionary is well-formed with all required parameters
        /// </summary>
        /// <param name="parameters">Dictionary(string,string)</param>
        public override void CopyFromDescription(Dictionary<string, string> parameters)
        {
            _nextQuestId = int.Parse(parameters["nextQuestId"]);
            string nextOwnerTag = parameters["nextQuestOwner"];
            _nextQuestOwner = QuestFramework.GetQuestFramework().GetQuestOwner(nextOwnerTag);
        }
    }
}