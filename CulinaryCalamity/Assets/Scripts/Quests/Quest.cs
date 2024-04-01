using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    /// <summary>
    /// Represents a quest which a player can complete.
    /// A player must satisify one or more criteria for a quest to be completed.
    /// Upon completion, one or more actions are taken (ie: adding money to player, adding item to player inventory, removing quest items from player inventory)
    /// </summary>
    public class Quest
    {
        private int _questID;
        private string _title;
        private TextAsset _dialogue;
        private List<QuestCompletionCriterion> _completionCriteria;
        private List<QuestCompletionAction> _questCompletionActions;

        /// <summary>
        /// Quest constructor
        /// </summary>
        /// <param name="questId">int, should be unique</param>
        /// <param name="dialogue">TextAsset, stores dialogue to queue upon quest completion</param>
        public Quest(int questId, string title, TextAsset dialogue, List<QuestCompletionCriterion> criteria, List<QuestCompletionAction> actions)
        {
            _questID = questId;
            _title = title;
            _dialogue = dialogue;
            _completionCriteria = criteria;
            _questCompletionActions = actions;
        }

        /// <summary>
        /// Getter for _questId
        /// </summary>
        /// <returns>int</returns>
        public int GetQuestId()
        {
            return _questID;
        }

        /// <summary>
        /// Getter for _dialogue
        /// </summary>
        /// <returns>TextAsset</returns>
        public TextAsset GetCompletionDialogue()
        {
            return _dialogue;
        }

        /// <summary>
        /// Checks if all the quests criteria are satisified but does NOT complete the quest
        /// </summary>
        /// <returns>bool, true if all criteria are satisified false otherwise</returns>
        public bool IsCompleteable()
        {
            foreach (QuestCompletionCriterion criterion in _completionCriteria)
            {
                if (!criterion.IsSatisfied(null))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Assumes that IsCompleteable is true
        /// Completes the quest by taking the various quest actions associated with the quest
        /// </summary>
        public void CompleteQuest()
        {
            foreach (QuestCompletionAction action in _questCompletionActions)
            {
                action.Take();
            }
        }
    }
}