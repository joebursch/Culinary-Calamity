namespace Quests
{
    /// <summary>
    /// Concrete implementation of QuestCompletionAction
    /// This action will add gold to a Player
    /// </summary>
    public class AddGoldAction : QuestCompletionAction
    {
        private int _goldToAdd;
        private Player _playerReceivingGold;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="goldToAdd">int</param>
        /// <param name="playerReceivingGold">Player</param>
        public AddGoldAction(int goldToAdd, Player playerReceivingGold)
        {
            _goldToAdd = goldToAdd;
            _playerReceivingGold = playerReceivingGold;
        }

        /// <summary>
        /// Concrete implementation of abstract Take method
        /// Adds gold to player object
        /// </summary>
        public override void Take()
        {
            _playerReceivingGold.AddGold(_goldToAdd);
        }
    }
}