using UnityEngine;
using System;
using Saving;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Linq;

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            QuestHandler[] questHandlers = FindObjectsByType<QuestHandler>(FindObjectsSortMode.None);
            foreach (QuestHandler qh in questHandlers)
            {
                if (qh.gameObject.CompareTag("RordanGamsay"))
                {
                    foreach (int questId in _questList.Keys)
                    {
                        qh.AssignQuestToHandle(questId);
                    }
                }
            }
        }


        /// <summary>
        /// Completes the specified questId belonging to the specified handler and owner
        /// </summary>
        /// <param name="questId">int, id of quest to complete. Assumed to be the id of a valid quest</param>
        /// <param name="handler">QuestHandler, handler for this quest. Assumed that questId matchines handler.HandledQuestId</param>
        /// <param name="owner">IQuestOwner, owner of the quest. Assumed that owner has a Quest with a matching quest id</param>
        public void CompleteQuest(int questId, QuestHandler handler, IQuestOwner owner)
        {
            handler.CompleteQuest(questId, owner.GetQuest(questId).GetCompletionDialogue());
            owner.CompleteQuest(questId);
            _questList.Remove(questId);
        }

        /// <summary>
        /// Creates a quest object based on id
        /// Assumes that a quest description is stored in Resources/QuestDescription/quest+questId
        /// </summary>
        /// <param name="questId">int, id of quest to create</param>
        /// <returns></returns>
        public Quest CreateQuest(int questId)
        {
            TextAsset questFile = Resources.Load<TextAsset>("QuestDescriptions/quest" + questId);
            Dictionary<string, object> questAttributes = JsonConvert.DeserializeObject<Dictionary<string, object>>(questFile.text);
            return new Quest(questId, questAttributes);
        }

        /// <summary>
        /// Assigns a quest to a quest owner
        /// Assumes that a quest description is stored in Resources/QuestDescription/quest+questId
        /// </summary>
        /// <param name="questId">int, id of quest to assign</param>
        /// <param name="owner">IQuestOwner</param>
        public void AssignQuest(int questId, IQuestOwner owner)
        {
            _questList.Add(questId, owner);
            owner.StartQuest(CreateQuest(questId));
            QuestHandler[] questHandlers = FindObjectsByType<QuestHandler>(FindObjectsSortMode.None);
            foreach (QuestHandler qh in questHandlers)
            {
                if (qh.gameObject.CompareTag("RordanGamsay"))
                {
                    Debug.Log(qh);
                    qh?.AssignQuestToHandle(questId);
                }
            }
        }

        /// <summary>
        /// Returns the quest owner corresponding to a certain tag
        /// Currently only accepts "MainCharacter" tag and returns the Player IQuestOwner
        /// Else returns null
        /// </summary>
        /// <param name="questOwnerTag">string, tag of questOwner to get</param>
        /// <returns>IQuestOwner</returns>
        public IQuestOwner GetQuestOwner(string questOwnerTag)
        {
            if (questOwnerTag == "MainCharacter")
            {
                return GameObject.Find("Player").GetComponent<Player>();
            }
            else { return null; }
        }

        /// <summary>
        /// Returns quest owner corresponding to questId
        /// </summary>
        /// <param name="questId">int</param>
        /// <returns>IQuestOwner, null if quest not tracked</returns>
        public IQuestOwner GetQuestOwner(int questId)
        {
            if (_questList.Keys.Contains(questId))
            {
                return _questList[questId];
            }
            else
            {
                return null;
            }
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