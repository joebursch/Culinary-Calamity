using System.Collections.Generic;
using UnityEngine;
using Items;


public class Npc : Character, InteractableObject
{
    private Item giveItem;

    private readonly Dictionary<string, string> dialogue;

    public void Interact()
    {
        Debug.Log("NPC --> Implement Later");
    }

}