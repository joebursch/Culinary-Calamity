using System.Collections.Generic;
using UnityEngine;


public class Questline : MonoBehaviour
{
    private List<Quest> quests; // Quests within the questline 
    private List<Item> rewards; // Rewards for completing the questline.
    private bool questlineCompleted; // Is the questline finished?
}