using System.Collections.Generic;
using UnityEngine;


public class Npc : Character, InteractableObject
{
    private Item giveItem;

    private readonly Dictionary<string, string> dialogue;

}