using UnityEngine;

namespace Quests
{
    public class QuestHandler : MonoBehaviour, InteractableObject
    {
        public int HandledQuestId { get; set; } = 0;

        public void Interact()
        {
            QuestFramework qf = QuestFramework.GetQuestFramework();
            if (qf.IsQuestCompleteable(HandledQuestId))
            {
                qf.CompleteQuest(HandledQuestId);
                string completionDialogue = qf.GetQuestCompletionDialogue(HandledQuestId);
                Debug.Log(completionDialogue);
                // Destroy(this);
            }
            else
            {
                gameObject.GetComponent<NPC>().Interact();
            }
        }
    }
}