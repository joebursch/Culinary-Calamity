using Attacks;
using Inventory;
using Items;
using Quests;
using Saving;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    enum PLAYER_SPD : int
    {
        Walk = 5,
        Run = 10,
    }

    #region Attributes
    // inventory
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private int _amountOfGold = 0;
    private PlayerInventory _playerInventory;
    private InventoryManager _inventoryManager;

    // quests
#pragma warning disable IDE0044, IDE0051
    private Questline _questline;
#pragma warning restore IDE0051, IDE0051
    // movement
    private bool _running;
    // layers
    [SerializeField] private LayerMask _itemsLayer;
    // input
    private Actions _controlScheme = null;
    // saving
    private ObjectSaveData _playerSaveData;
    // combat 
    private AttackStrategy _attackStrategy;
    #endregion

    #region UnityBuiltIn
    void Awake()
    {
        _playerInventory = new PlayerInventory();
        movementSpeed = (int)PLAYER_SPD.Walk;
        _controlScheme = new Actions();
        characterAnimator = GetComponent<Animator>();
        _playerSaveData = new();
        currentHealth = characterHealth;
        _attackStrategy = new MeleeAttack(0.25f, LayerMask.GetMask("Enemies")); // Should probably grab damage from the equipt weapon when thats done
    }

    void Start()
    {
        try
        {
            GameSaveManager.GetGameSaveManager().Save += OnSave;
            GameSaveManager.GetGameSaveManager().Load += OnLoad;
        }
        catch (NullReferenceException)
        {
            Debug.Log("No Game Save Manager Found");
        }
    }

    void OnEnable() => _controlScheme.Standard.Enable();

    void OnDestroy() => _controlScheme.Standard.Disable();

    void Update()
    {
        MovePlayer();
        if (_controlScheme.Standard.Interact.triggered) { Interact(); }
        if (_controlScheme.Standard.OpenInventory.triggered) { ToggleInventory(); }
        if (_controlScheme.Standard.Attack.triggered)
        {
            _attackStrategy.Attack(FindTarget());
            characterAnimator.Play("Attack");
        }
    }
    #endregion

    #region Saving
    /// <summary>
    /// Used in starting a new save. Just sets a players name.
    /// </summary>
    /// <param name="playerName"></param>
    /// <returns></returns>
    public static ObjectSaveData CreateInitialPlayerSaveData(string playerName)
    {
        ObjectSaveData playerSaveData = new();
        Dictionary<string, string> playerData = new()
        {
            { "PlayerName", playerName }
        };
        playerSaveData.UpdateSaveData(playerData);
        return playerSaveData;
    }

    /// <summary>
    /// Listener for the Save event. Pushes current state of the player to the GameSaveManager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnSave(object sender, EventArgs e)
    {
        Dictionary<string, string> playerData = new()
        {
            { "PlayerName", characterName },
            { "PlayerGold", _amountOfGold.ToString()}
        };

        _playerSaveData.UpdateSaveData(playerData);
        GameSaveManager.GetGameSaveManager().UpdateObjectSaveData("PlayerObject", _playerSaveData);
    }

    /// <summary>
    /// Listener for the load event. Pulls save data from GameSaveManager and performs appropriate updates
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnLoad(object sender, EventArgs e)
    {
        _playerSaveData = GameSaveManager.GetGameSaveManager().GetObjectSaveData("PlayerObject");
        characterName = _playerSaveData.SaveData["PlayerName"];
        _playerSaveData.SaveData.TryGetValue("PlayerGold", out string gold);
        if (gold != null) { _amountOfGold = int.Parse(gold); }
    }
    #endregion

    #region Movement
    /// <summary>
    /// Method for facilitating player movement. 
    /// </summary>
    void MovePlayer()
    {
        _movementDir = GetMovementDirection();
        CheckRunning();
        ConfigureAnimator(_movementDir, _running);
        if (IsWalkable(_movementDir))
        {
            transform.Translate(movementSpeed * Time.deltaTime * _movementDir);
        }
    }

    /// <summary>
    /// Method for receiving player input. Restricts diagonal movement. 
    /// </summary>
    /// <returns>Vector2 representing direction of player movement</returns>
    private Vector2 GetMovementDirection()
    {
        var tempDir = _controlScheme.Standard.Move.ReadValue<Vector2>();
        if (tempDir.x > 0 | tempDir.x < 0) { tempDir.y = 0; }
        return tempDir;
    }

    /// <summary>
    /// Checks to see if the player has toggled sprinting. Updates movement speed accordingly. 
    /// </summary>
    private void CheckRunning()
    {
        if (_controlScheme.Standard.Run.triggered)
        {
            _running = !_running;
#pragma warning disable IDE0066
            switch (movementSpeed)
            {
                case (int)PLAYER_SPD.Walk:
                    movementSpeed = (int)PLAYER_SPD.Run;
                    break;
                default:
                    movementSpeed = (int)PLAYER_SPD.Walk;
                    break;
            }
#pragma warning restore IDE0066
        }
    }
    #endregion

    #region Interaction
    /// <summary>
    /// When the player presses 'E', check for an interactable object in the facing direction. 
    /// </summary>
    private void Interact()
    {
        var facingDir = new Vector3(characterAnimator.GetFloat("moveX"), characterAnimator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, _interactableObjectsLayer);
        if (collider != null) { collider.GetComponent<InteractableObject>()?.Interact(); }
    }

    /// <summary>
    /// Executes when the player collides with something
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            _playerInventory.AddItem(item.GetItemId());
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("InteractableObjects"))
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                Door tempDoor = collision.gameObject.GetComponent<Door>();

                if (!tempDoor.IsActive())
                {
                    if (SceneManager.GetActiveScene().name == tempDoor.GetDestinationSceneName())
                    {
                        transform.position = tempDoor.GetDestinationLocation();
                    }
                    else
                    {
                        SceneManager.LoadScene(tempDoor.GetDestinationSceneName());
                        transform.position = tempDoor.GetDestinationLocation();
                    }
                }
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Projectiles"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().GetProjectileDamage());
        }
    }
    #endregion

    #region Inventory
    /// <summary>
    /// Instantiates the inventory menu as a child of player and returns a reference to it
    /// </summary>
    /// <returns></returns>
    private InventoryManager CreateInventoryDisplay()
    {
        GameObject display = Instantiate(_inventoryPrefab, gameObject.transform);
        display.SetActive(false);
        InventoryManager displayMngr = display.GetComponent<InventoryManager>();
        displayMngr.SetPlayerName(characterName);
        displayMngr.SetInventory(_playerInventory);
        return displayMngr;
    }

    /// <summary>
    /// Activates/Deactivates the inventory screen to display/hide it
    /// </summary>
    private void ToggleInventory()
    {
        if (_inventoryManager == null)
        {
            _inventoryManager = CreateInventoryDisplay();
            _inventoryManager.InventoryClose += OnInventoryClose;
        }

        _inventoryManager.ToggleInventory();
    }

    /// <summary>
    /// Handles the InventoryClose event - necessary to allow closing from inventory menu button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnInventoryClose(object sender, EventArgs e)
    {
        ToggleInventory();
    }
    #endregion

    #region Combat

    /// <summary>
    /// Finds the location directly in front of the player. This will be where the player targets. 
    /// </summary>
    /// <returns>Vector3 of target location</returns>
    private Vector3 FindTarget()
    {
        var targetPosition = new Vector3(transform.position.x + characterAnimator.GetFloat("moveX"), transform.position.y + characterAnimator.GetFloat("moveY"));
        return targetPosition;
    }
    protected override void KnockbackEffect()
    {
        // For now, do nothing... Do we want the player to be knocked back?
    }
    /// <summary>
    /// Method for dying...
    /// </summary>
    protected override void Death()
    {
        // What do we need to do when we die?
        Debug.Log("I have died!");
    }

    #endregion
}