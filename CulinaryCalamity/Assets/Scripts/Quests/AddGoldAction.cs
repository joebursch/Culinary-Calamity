using System.Collections.Generic;

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
        /// Concrete implementation of abstract Take method
        /// Adds gold to player object
        /// </summary>
        public override void TakeAction()
        {
            _playerReceivingGold.AddGold(_goldToAdd);
        }

        /// <summary>
        /// Assigns values based on parameters based. Assumes parameter dictionary is well-formed with all required parameters
        /// </summary>
        /// <param name="parameters">Dictionary(string,string)</param>
        public override void CopyFromDescription(Dictionary<string, string> parameters)
        {
            _goldToAdd = int.Parse(parameters["goldToAdd"]);
            string playerTag = parameters["playerReceivingGold"];
            _playerReceivingGold = (Player)QuestFramework.GetQuestFramework().GetQuestOwner(playerTag);
        }
    }
}