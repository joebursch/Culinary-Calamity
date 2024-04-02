using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Quests
{
    /// <summary>
    /// Abstract class representing an action to be taken upon completing a quest
    /// ie: unlocking a recipe, adding gold to inventory, removing quest items from inventory, etc.
    /// </summary>
    public abstract class QuestCompletionAction
    {
        /// <summary>
        /// Abstract method representing 'taking' the action.
        /// </summary>
        public abstract void Take();

        /// <summary>
        /// Populates an existing Quest Completion Action with values from a dictionary
        /// </summary>
        /// <param name="actionDescription">Dictionary(string, string) containing quest action parameters </param>
        public abstract void CopyFromDescription(Dictionary<string, string> parameters);
    }
}