
using System.Collections.Generic;
using UnityEngine;

public class Forageable : MonoBehaviour, InteractableObject
{
    // total wait time until forageable can be harvested again
    private int totalRespawnTime;
    // true if you can harvest an item from the forageable
    private bool isSpawned;
    // countdown from totalRespawnTime to 0
    private int currentTimeToRespawn;
    // 1 or more items dropped when the forageable is harvested
    private List<Item> itemsDropped;
}