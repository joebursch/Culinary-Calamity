using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Quests
{
    public class Questline : MonoBehaviour
    {
        private List<Quest> questlineQuests;
        private List<Item> questlineRewards;
        private bool questlineCompleted;
    }
}