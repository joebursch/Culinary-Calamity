
using System.Collections.Generic;
using UnityEngine;

public class Forageable : MonoBehaviour, InteractableObject
{
    // total wait time until forageable can be harvested again
    private int _totalRespawnTime;
    // true if you can harvest an item from the forageable
    private bool _isSpawned;
    // countdown from totalRespawnTime to 0
    private int _currentTimeToRespawn;
    // 1 or more items dropped when the forageable is harvested
    [SerializeField] private List<GameObject> _itemsDropped;


    public void Interact()
    {
        Debug.Log("Forageable --> Implement Later");
    }


}