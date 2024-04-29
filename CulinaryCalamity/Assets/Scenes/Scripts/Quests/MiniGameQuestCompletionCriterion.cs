using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionCriterion
    /// Represents the criterion for a gathering task
    /// </summary>
    public class MiniGameQuestCompletionCriterion : QuestCompletionCriterion
    {
        private bool _miniGameCompleted = false;

        public MiniGameQuestCompletionCriterion()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        /// <summary>
        /// Determines if the questOwner has satisfied the criterion by collecting the appropriate type and quantity of items
        /// The questOwner is assumed to be a Player object (which implements IQuestOwner)
        /// </summary>
        /// <param name="questOwner">IQuestOwner</param>
        /// <returns>bool, true if the questOwner has the correct type and number of items in their inventory</returns>
        public override bool IsSatisfied(IQuestOwner questOwner)
        {
            return _miniGameCompleted;
        }

        // <summary>
        /// Assigns values based on parameters based. Assumes parameter dictionary is well-formed with all required parameters
        /// </summary>
        /// <param name="parameters">Dictionary(string,string)</param>
        public override void CopyFromDescription(Dictionary<string, string> parameters) { return; }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MiniGame")
            {
                MiniGameManager.instance.MiniGameCompleted += (object sender, EventArgs e) => { _miniGameCompleted = true; };
            }
        }
    }
}