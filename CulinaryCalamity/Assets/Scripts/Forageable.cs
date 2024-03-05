
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

    private Vector3 _randomSpawnPosition;

    void Awake()
    {
        _randomSpawnPosition = Vector3.zero;
    }
    public void Interact()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        foreach (GameObject gameObject in _itemsDropped)
        {
            SetRandomSpawnPosition();
            Instantiate(gameObject, transform.position + _randomSpawnPosition, Quaternion.identity);
        }
    }

    private void SetRandomSpawnPosition()
    {
        _randomSpawnPosition.x = Random.Range(-4.0f, 4.0f);
        _randomSpawnPosition.y = Random.Range(-2.0f, 2.0f);
    }
}