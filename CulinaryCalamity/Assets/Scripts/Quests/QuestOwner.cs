namespace Quests
{
    public interface IQuestOwner
    {
        public Quest OwnedQuest { get; protected set; }
    }
}