using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Quests
{
    public class Quest
    {
        private int _questID;
        private List<QuestCompletionCriterion> _completionCriteria;

        // private Dictionary<string, object> questAttributes; // <"name", "Quest Name">, <"reward", List<Item> questReward>, etc.

        public bool IsCompleteable()
        {
            foreach (QuestCompletionCriterion criterion in _completionCriteria)
            {
                if (!criterion.IsSatisfied())
                {
                    return false;
                }
            }
            return true;
        }

        public string GetCompletionDialogue() { return ""; }

        public void CompleteQuest() { }
    }
}