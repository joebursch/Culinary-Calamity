using System.Collections.Generic;
using Dialogue;
using UnityEngine;

namespace Quests
{
    /// <summary>
    /// Object that a quest owner reports to to complete a quest. 
    /// ie: Rordan Gamsay is a QuestHandler for main story quests
    /// </summary>
    public class QuestHandler : MonoBehaviour
    {
        private List<int> _handledQuestIds;

        void Awake()
        {
            _handledQuestIds = new();
        }
        /// <summary>
        /// Complete the Quest Handlers responsibilities for a quest
        /// </summary>
        /// <param name="questId">int, ID of quest to complete. Assumed to be a valid questId contained in _handledQuestIds</param>
        /// <param name="dialogue">TextAsset, dialogue resource to play on quest completion</param>
        public void CompleteQuest(int questId, TextAsset dialogue)
        {
            gameObject.GetComponent<NPC>().SetInteractionFacingDirection();
            DialogueManager.GetDialogueManager().InitializeDialogue(dialogue);
            RemoveHandledQuest(questId);
        }

        /// <summary>
        /// Assign a quest to handle
        /// </summary>
        /// <param name="questId">int, assumed to correspond uniquely to a valid quest</param>
        public void AssignQuestToHandle(int questId)
        {
            _handledQuestIds.Add(questId);
        }

        /// <summary>
        /// Checks if this quest handler handles a particular quest
        /// </summary>
        /// <param name="questId">int, quest id to check for</param>
        /// <returns>bool, true if handled, false if not</returns>
        public bool IsQuestHandled(int questId)
        {
            return _handledQuestIds.Contains(questId);
        }

        /// <summary>
        /// removes quest from list of handled quest - done on completion
        /// </summary>
        /// <param name="questId">int, id of quest to remove</param>
        private void RemoveHandledQuest(int questId)
        {
            _handledQuestIds.Remove(questId);
        }
    }
}