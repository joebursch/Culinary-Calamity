using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Quests
{
    public class Quest
    {
        private int _questID;
        // private Dictionary<string, object> questAttributes; // <"name", "Quest Name">, <"reward", List<Item> questReward>, etc.

        public bool IsCompleteable() { return false; }

        public string GetCompletionDialogue() { return ""; }

        public void CompleteQuest() { }
    }
}