using System.Collections;
using System.Collections.Generic;
using Quests;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

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
