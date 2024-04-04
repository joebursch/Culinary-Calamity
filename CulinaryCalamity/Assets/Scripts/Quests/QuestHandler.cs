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
        public int HandledQuestId { get; set; } = 0; // id of quest being handled

        /// <summary>
        /// Plays dialogue upon quest completion
        /// </summary>
        /// <param name="dialogue"></param>
        public void StartQuestCompletionDialogue(TextAsset dialogue)
        {
            DialogueManager.GetDialogueManager().InitializeDialogue(dialogue);
        }
    }
}