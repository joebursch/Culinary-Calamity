using UnityEditorInternal;
using UnityEngine;
using System;
using Saving;
using System.Collections.Generic;

namespace Quests
{
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnSave(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Listener for the load event. Pulls save data from GameSaveManager and performs appropriate updates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnLoad(object sender, EventArgs e)
        {

        }

        public bool IsQuestCompleteable(int questId)
        {
            try
            {
                return _questList[questId].OwnedQuest.IsCompleteable();
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public void CompleteQuest(int questId)
        {
            _questList[questId].OwnedQuest.CompleteQuest();
        }

        public string GetQuestCompletionDialogue(int questId)
        {
            return _questList[questId].OwnedQuest.GetCompletionDialogue();
        }

        public static QuestFramework GetQuestFramework()
        {
            return _questFramework;
        }
    }
}