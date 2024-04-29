
using Quests;
using UnityEngine;

/// <summary>
/// Used in testing scene - not used in production
/// </summary>
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
        try
        {
            QuestFramework.GetQuestFramework().AssignQuest(0, owner);
        }
        catch
        {
            return;
        }
    }
}
