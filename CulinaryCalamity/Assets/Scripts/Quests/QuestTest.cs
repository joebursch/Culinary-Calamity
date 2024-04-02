using Quests;
using UnityEngine;

public class QuestTest : MonoBehaviour
{
    [SerializeField] GameObject player;
    private IQuestOwner owner;

    void Awake()
    {
        owner = player.GetComponent<IQuestOwner>();
    }
    void Start()
    {
        QuestFramework.GetQuestFramework().AssignQuest(0, owner);
    }
}
