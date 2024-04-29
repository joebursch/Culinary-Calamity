using Items;
using UnityEngine;

public class Forageable : MonoBehaviour, InteractableObject
{
    // total wait time until forageable can be harvested again
    [SerializeField] private int _totalRespawnTime;
    [SerializeField] private int _itemIdToDrop; // must be int to assign in editor, must be cast to ItemId enum when used
    [SerializeField] private Vector3 _minSpawnDistance;
    [SerializeField] private int _minNumberOfDrops;
    [SerializeField] private int _maxNumberOfDrops;
    // amount of time passed since item drops
    private float _respawnTimer;
    // true if the forageable has spawned items to be picked up
    private bool _itemsSpawned;
    private Vector3 _randomSpawnPosition;
    private Animator _forageableAnimator;

    void Awake()
    {
        _randomSpawnPosition = Vector3.zero;
        _forageableAnimator = GetComponent<Animator>();
    }
    /// <summary>
    /// Response to Player interaction. Spawn items and/or play correct animation. 
    /// </summary>
    public void Interact()
    {
        if (!_itemsSpawned) { SpawnItems(); }
        ConfigureInteractAnimation(_itemsSpawned);
    }
    /// <summary>
    /// Spawn all of the available items.
    /// </summary>
    private void SpawnItems()
    {
        var numItemsToDrop = Random.Range(_minNumberOfDrops, _maxNumberOfDrops);
        for (int i = 0; i < numItemsToDrop; i++)
        {
            SetRandomSpawnPosition();
            ItemManager.GetItemManager().SpawnItem((ItemId)_itemIdToDrop, transform.position + _randomSpawnPosition + _minSpawnDistance, Quaternion.identity);
        }
        _itemsSpawned = true;
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
        if (_respawnTimer > _totalRespawnTime)
        {
            _forageableAnimator.Play("Idle");
            _respawnTimer = 0.0f;
            _itemsSpawned = false;
        }
    }
    void Update()
    {
        if (_itemsSpawned)
        {
            _respawnTimer += Time.deltaTime;
            CheckRespawn();
        }
    }
}
