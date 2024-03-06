
using System.Collections.Generic;
using UnityEngine;

public class Forageable : MonoBehaviour, InteractableObject
{
    // total wait time until forageable can be harvested again
    [SerializeField] private int _totalRespawnTime;
    [SerializeField] private GameObject _itemToDrop;
    // 1 or more items dropped when the forageable is harvested
    [SerializeField] private List<GameObject> _itemsDropped;
    // amount of time passed since item drops
    private float _respawnTimer;
    // true if you can harvest an item from the forageable
    private bool _isSpawned;
    private Vector3 _randomSpawnPosition;
    private Animator _forageableAnimator;

    void Awake()
    {
        LoadItemsToDrop();
        _randomSpawnPosition = Vector3.zero;
        _forageableAnimator = GetComponent<Animator>();
    }
    /// <summary>
    /// Response to Player interaction. Spawn items and/or play correct animation. 
    /// </summary>
    public void Interact()
    {
        if (_itemsDropped.Count > 0) { SpawnItems(); }
        ConfigureInteractAnimation(_isSpawned);
    }
    /// <summary>
    /// Sets a random amount of items to be dropped. 
    /// </summary>
    private void LoadItemsToDrop()
    {
        var numItemsToAdd = Random.Range(1, 4);
        for (int i = 0; i < numItemsToAdd; i++)
        {
            _itemsDropped.Add(_itemToDrop);
        }
    }
    /// <summary>
    /// Spawn all of the available items.
    /// </summary>
    private void SpawnItems()
    {
        for (int i = 0; i < _itemsDropped.Count; i++)
        {
            SetRandomSpawnPosition();
            Instantiate(_itemToDrop, transform.position + _randomSpawnPosition, Quaternion.identity);
            _itemsDropped.Remove(_itemToDrop);
        }
        _isSpawned = true;
    }
    /// <summary>
    /// Set a random spawn location for the item drops
    /// </summary>
    private void SetRandomSpawnPosition()
    {
        _randomSpawnPosition.x = Random.Range(-4.0f, 4.0f);
        _randomSpawnPosition.y = Random.Range(-2.0f, 2.0f);
    }
    /// <summary>
    /// Play the correct animation based on forageable state. 
    /// </summary>
    /// <param name="spawned">Boolean for if items are spawned</param>
    private void ConfigureInteractAnimation(bool spawned)
    {
        switch (spawned)
        {
            case false:
                _forageableAnimator.Play("InteractFull");
                break;
            default:
                _forageableAnimator.Play("InteractEmpty");
                break;
        }
    }
    /// <summary>
    /// Checks to see if respawn time has elapsed.
    /// </summary>
    private void CheckRespawn()
    {
        if (_isSpawned && (int)_respawnTimer > _totalRespawnTime)
        {
            _forageableAnimator.Play("Idle");
            _respawnTimer = 0.0f;
            ResetItems();
        }
    }
    /// <summary>
    /// Resets the items to be spawned. 
    /// </summary>
    private void ResetItems()
    {
        LoadItemsToDrop();
        _isSpawned = false;
    }

    void Update()
    {
        _respawnTimer += Time.deltaTime;
        CheckRespawn();
    }
}