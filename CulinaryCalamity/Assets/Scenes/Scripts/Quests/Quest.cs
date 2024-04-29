using System;
using System.Collections.Generic;
using Items;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Quests
{
    /// <summary>
    /// Represents a quest which a player can complete.
    /// A player must satisify one or more criteria for a quest to be completed.
    /// Upon completion, one or more actions are taken (ie: adding money to player, adding item to player inventory, removing quest items from player inventory)
    /// </summary>
    public class Quest
    {
        private int _questID;
        private string _title;
        private TextAsset _endingDialogue;
        private List<QuestCompletionCriterion> _completionCriteria;
        private List<QuestCompletionAction> _questCompletionActions;

        /// <summary>
        /// Quest constructor
        /// </summary>
        /// <param name="questId">int, should be unique</param>
        /// <param name="dialogue">TextAsset, stores dialogue to queue upon quest completion</param>
        public Quest(int questId, string title, TextAsset dialogue, List<QuestCompletionCriterion> criteria, List<QuestCompletionAction> actions)
        {
            _questID = questId;
            _title = title;
            _endingDialogue = dialogue;
            _completionCriteria = criteria;
            _questCompletionActions = actions;
        }

        /// <summary>
        /// Quest constructor from quest description json
        /// </summary>
        /// <param name="parameters">Dictionary(string, object), JSON of quest attributes. See Resources/QuestDescriptions folder for examples of format.</param>
        public Quest(int questId, Dictionary<string, object> parameters)
        {
            _questID = questId;
            _title = (string)parameters["QuestTitle"];

            string endDialoguePath = (string)parameters["QuestEndDialoguePath"];
            _endingDialogue = Resources.Load<TextAsset>(endDialoguePath);

            _completionCriteria = new();

            foreach (Dictionary<string, object> criterion in ((JArray)parameters["QuestCompletionCriteria"]).ToObject<Dictionary<string, object>[]>())
            {
                QuestCompletionCriterion temp = (string)criterion["Type"] switch
                {
                    "GatheringQuestCompletionCriterion" => new GatheringQuestCompletionCriterion(),
                    "MiniGameQuestCompletionCriterion" => new MiniGameQuestCompletionCriterion(),
                    _ => null,
                };
                temp.CopyFromDescription(((JObject)criterion["Parameters"]).ToObject<Dictionary<string, string>>());
                _completionCriteria.Add(temp);
            }

            _questCompletionActions = new();
            foreach (Dictionary<string, object> action in ((JArray)parameters["QuestCompletionActions"]).ToObject<Dictionary<string, object>[]>())
            {
                QuestCompletionAction temp = (string)action["Type"] switch
                {
                    "StartNextQuestAction" => new StartNextQuestAction(),
                    "AddGoldAction" => new AddGoldAction(),
                    "AddItemAction" => new AddItemAction(),
                    _ => null,
                };
                temp.CopyFromDescription(((JObject)action["Parameters"]).ToObject<Dictionary<string, string>>());
                _questCompletionActions.Add(temp);
            }
        }

        /// <summary>
        /// Getter for _questId
        /// </summary>
        /// <returns>int</returns>
        public int GetQuestId()
        {
            return _questID;
        }

        /// <summary>
        /// Getter for _dialogue
        /// </summary>
        /// <returns>TextAsset</returns>
        public TextAsset GetCompletionDialogue()
        {
            return _endingDialogue;
        }

        /// <summary>
        /// Checks if all the quests criteria are satisified but does NOT complete the quest
        /// </summary>
        /// <returns>bool, true if all criteria are satisified false otherwise</returns>
        public bool IsCompleteable()
        {
            foreach (QuestCompletionCriterion criterion in _completionCriteria)
            {
                if (!criterion.IsSatisfied(QuestFramework.GetQuestFramework().GetQuestOwner(_questID)))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Assumes that IsCompleteable is true
        /// Completes the quest by taking the various quest actions associated with the quest
        /// </summary>
        public void CompleteQuest()
        {
            foreach (QuestCompletionAction action in _questCompletionActions)
            {
                action.TakeAction();
            }
        }

        /// <summary>
        /// Getter for title attribute
        /// </summary>
        /// <returns>string</returns>
        public string GetTitle()
        {
            return _title;
        }
    }
}