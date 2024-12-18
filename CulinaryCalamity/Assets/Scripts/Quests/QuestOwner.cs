using System.Collections.Generic;

namespace Quests
{
    /// <summary>
    /// Interface for 'owner' of quest or someone who gets assigned quests to complete
    /// ie: Player implements questOwner
    /// </summary>
    public interface IQuestOwner
    {
        // list of currently assigned quests
        public List<Quest> OwnedQuests { get; set; }
        public List<int> CompletedQuestIds { get; set; }

        /// <summary>
        /// Returns OwnedQuest with the corresponding id
        /// </summary>
        /// <param name="questId">int, id of quest to get.</param>
        /// <returns>Quest, returns null if questId is not found in OwnedQuests</returns>
        public Quest GetQuest(int questId)
        {
            foreach (Quest quest in OwnedQuests)
            {
                if (quest.GetQuestId() == questId)
                {
                    return quest;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a quest to the OwnedQuests list
        /// </summary>
        /// <param name="newQuest">Quest to add</param>
        public void StartQuest(Quest newQuest)
        {
            OwnedQuests.Add(newQuest);
        }

        /// <summary>
        /// Completes an owned quest. Takes no action if the questId does not correspond to an OwnedQuest.
        /// Assumes that the corresponding quest is completeable.
        /// </summary>
        /// <param name="questId">int</param>
        public void CompleteQuest(int questId)
        {
            Quest q = GetQuest(questId);
            if (q != null)
            {
                q.CompleteQuest();
                OwnedQuests.Remove(q);
            }
            CompletedQuestIds.Add(questId);
        }

        /// <summary>
        /// Checks if a quest is completable. Returns false if the questId does correspond to an owned quest
        /// </summary>
        /// <param name="questId">int</param>
        /// <returns>bool, true if quest is owned and completable, false otherwise</returns>
        public bool IsQuestCompleteable(int questId)
        {
            Quest q = GetQuest(questId);
            return q != null && q.IsCompleteable();
        }

        /// <summary>
        /// Returns the id of a quest owned by this IQuestOwner and handled by the provider QuestHandler
        /// </summary>
        /// <param name="qh">QuestHandler</param>
        /// <returns>int, -1 if no matching id - quest id if mathcing</returns>
        public int GetHandledQuestId(QuestHandler qh)
        {
            foreach (Quest q in OwnedQuests)
            {
                if (qh.IsQuestHandled(q.GetQuestId()))
                {
                    return q.GetQuestId();
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks if this quest owner has completed a particular quest based on the quest id.
        /// Returns true if questId has been completed, false in any other circumstance.
        /// </summary>
        /// <param name="questId">int</param>
        /// <returns>bool</returns>
        public bool IsQuestCompleted(int questId)
        {
            return CompletedQuestIds.Contains(questId);
        }
    }
}