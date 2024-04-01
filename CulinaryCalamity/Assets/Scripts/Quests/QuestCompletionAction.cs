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
    }
}