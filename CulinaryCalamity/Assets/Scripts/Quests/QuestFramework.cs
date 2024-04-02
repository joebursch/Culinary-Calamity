
using UnityEngine;
using System;
using Saving;
using System.Collections.Generic;

namespace Quests
{
    /// <summary>
    /// Singleton. Used to create, assign, save, load, and coordinate completion of quests
    /// </summary>
    public class QuestFramework : MonoBehaviour
    {
        private static QuestFramework _questFramework;
        private ObjectSaveData _questSaveData;
        private Dictionary<int, IQuestOwner> _questList;

        private void Awake()
        {
            if (_questFramework == null)
            {
                _questFramework = this;
            }
            else
            {
                Destroy(gameObject);
            }
            _questSaveData = new();
            _questList = new();
        }

        private void Start()
        {
            try
            {
                GameSaveManager.GetGameSaveManager().Save += OnSave;
                GameSaveManager.GetGameSaveManager().Load += OnLoad;
            }
            catch (NullReferenceException)
            {
                Debug.Log("No Game Save Manager Found");
            }
        }

        /// <summary>
        /// Listener for the Save event. Pushes current state of the QuestFramework to the GameSaveManager
        /// </summary>
        /// <param name="sender">object, this</param>
        /// <param name="e">EventArgs, EventArgs.Empty</param>
        public void OnSave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Listener for the load event. Pulls save data from GameSaveManager and performs appropriate updates
        /// </summary>
        /// <param name="sender">object, this</param>
        /// <param name="e">EventArgs, EventArgs.Empty</param>
        public void OnLoad(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Completes the specified questId belonging to the specified handler and owner
        /// </summary>
        /// <param name="questId">int, id of quest to complete. Assumed to be the id of a valid quest</param>
        /// <param name="handler">QuestHandler, handler for this quest. Assumed that questId matchines handler.HandledQuestId</param>
        /// <param name="owner">IQuestOwner, owner of the quest. Assumed that owner has a Quest with a matching quest id</param>
        public void CompleteQuest(int questId, QuestHandler handler, IQuestOwner owner)
        {
            handler.StartQuestCompletionDialogue(owner.GetQuest(questId).GetCompletionDialogue());
            owner.CompleteQuest(questId);
            _questList.Remove(questId);
        }

        public Quest CreateQuest()
        {
            return null;
        }

        public void AssignQuest(Quest quest, IQuestOwner owner)
        {
            _questList.Add(quest.GetQuestId(), owner);
            owner.StartQuest(quest);
        }

        /// <summary>
        /// For singleton pattern.
        /// </summary>
        /// <returns>QuestFramework, reference to singleton</returns>
        public static QuestFramework GetQuestFramework()
        {
            return _questFramework;
        }
    }
}