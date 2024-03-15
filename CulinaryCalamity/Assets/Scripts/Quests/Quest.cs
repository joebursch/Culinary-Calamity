using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class Quest : MonoBehaviour
    {
        private int questID;
        private Dictionary<string, object> questAttributes; // <"name", "Quest Name">, <"reward", List<Item> questReward>, etc.
    }
}