using System;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using Inventory;
using Quests;
using Items;

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
    [SerializeField] private int _amountOfGold;
    private PlayerInventory _playerInventory;
    private InventoryManager _inventoryManager;

    // quests
    private Questline _questline;
    // movement
    private Vector2 _movementDir;
    private bool _running;
    // layers
    [SerializeField] private LayerMask _solidObjectsLayer;
    [SerializeField] private LayerMask _interactableObjectsLayer;
    [SerializeField] private LayerMask _itemsLayer;
    // input
    private Actions _controlScheme = null;
    // saving
    private ObjectSaveData playerSaveData;
    #endregion

    #region UnityBuiltIn
    void Awake()
    {
        _playerInventory = new PlayerInventory();
        movementSpeed = (int)PLAYER_SPD.Walk;
        _controlScheme = new Actions();
        characterAnimator = GetComponent<Animator>();
        playerSaveData = new();
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
    }
    #endregion

    #region Saving
    /// <summary>
    /// Listener for the Save event. Pushes current state of the player to the GameSaveManager
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnSave(object sender, EventArgs e)
    {
        Dictionary<string, string> playerData = new()
        {
            { "PlayerName", characterName }
        };

        playerSaveData.UpdateSaveData(playerData);
        GameSaveManager.GetGameSaveManager().UpdateObjectSaveData("PlayerObject", playerSaveData);
    }

    /// <summary>
    /// Listener for the load event. Pulls save data from GameSaveManager and performs appropriate updates
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnLoad(object sender, EventArgs e)
    {
        playerSaveData = GameSaveManager.GetGameSaveManager().GetObjectSaveData("PlayerObject");
        characterName = playerSaveData.SaveData["PlayerName"];
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
        ConfigureAnimator();
        if (IsWalkable())
        {
            transform.Translate(_movementDir * movementSpeed * Time.deltaTime);
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
            switch (movementSpeed)
            {
                case (int)PLAYER_SPD.Walk:
                    movementSpeed = (int)PLAYER_SPD.Run;
                    break;
                default:
                    movementSpeed = (int)PLAYER_SPD.Walk;
                    break;
            }
        }
    }

    /// <summary>
    /// Method for triggering correct animations. 
    /// </summary>
    private void ConfigureAnimator()
    {
        bool moving = _movementDir != Vector2.zero;
        // Only update floats when there is movement input. Otherwise, sprite snaps back to facing camera. 
        if (moving)
        {
            characterAnimator.SetFloat("moveX", _movementDir.x);
            characterAnimator.SetFloat("moveY", _movementDir.y);
        }
        characterAnimator.SetBool("isRunning", _running && moving);
        characterAnimator.SetBool("isWalking", moving);
    }

    /// <summary>
    /// Determines if the area the player is moving towards is obstructed. 
    /// </summary>
    /// <returns>A boolean value that determines if the player can move</returns>
    private bool IsWalkable()
    {
        var targetPos = transform.position;
        targetPos.x += _movementDir.x;
        targetPos.y += _movementDir.y;
        if (Physics2D.OverlapCircle(targetPos, 0.2f, _interactableObjectsLayer | _solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
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
}