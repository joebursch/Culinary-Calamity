using System.Collections.Generic;
using UnityEngine;


public class Questline : MonoBehaviour
{
    private Dictionary<int, object[]> quests; // <int questID, [*quest stats*]>; // Quests within the questline 
    private Item[] rewards; // Rewards for completing the questline.
    private bool completed; // Is the questline finished?
}