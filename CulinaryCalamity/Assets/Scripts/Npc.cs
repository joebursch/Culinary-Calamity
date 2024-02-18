using System.Collections.Generic;
using UnityEngine;


public class Npc : Character
{
    private Item giveItem;

    public Npc(string name, Sprite sprite, float speed, List<Animation> animations, Item giveItem) 
        : base(name, sprite, speed, animations)
    {
        this.giveItem = giveItem;
    }
}