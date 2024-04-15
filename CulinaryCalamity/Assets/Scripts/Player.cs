using Attacks;
using Dialogue;
using Inventory;
using Items;
using Quests;
using Saving;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character, IQuestOwner
{
    enum PLAYER_SPD : int
    {
        Walk = 8,
        Run = 16,
    }

    #region Attributes
    private static GameObject _playerInstance;

    // inventory
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private int _amountOfGold = 0;
    private PlayerInventory _playerInventory;
    private InventoryManager _inventoryManager;

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
    // quests
    public List<Quest> OwnedQuests { get; set; }
    private QuestMenuManager _questMenuManager;
    [SerializeField] private GameObject _questMenuPrefab;
    #endregion

    #region UnityBuiltIn
    void Awake()
    {
        if (_playerInstance == null)
        {
            _playerInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        movementSpeed = (int)PLAYER_SPD.Walk;
        _controlScheme = new Actions();
        characterAnimator = GetComponent<Animator>();
        _playerSaveData = new();

        currentHealth = characterHealth;
        _attackStrategy = new MeleeAttack(0.25f, LayerMask.GetMask("Enemies")); // Should probably grab damage from the equipt weapon when thats done

        OwnedQuests = new();
    }

    void Start()
    {
        _playerInventory = new PlayerInventory();
        try
        {
            GameSaveManager.GetGameSaveManager().Save += OnSave;
            GameSaveManager.GetGameSaveManager().Load += OnLoad;
        }
        catch (NullReferenceException)
        {
            Debug.Log("No Game Save Manager Found");
        }

        DialogueCanvasManager.GetDialogueCanvasManager().DisplayActivated += ActivateDialogueControls;
        DialogueCanvasManager.GetDialogueCanvasManager().DisplayDeactivated += ActivateStandardControls;
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
        if (_controlScheme.Standard.OpenQuestMenu.triggered) { ToggleQuestMenu(); }
        if (_controlScheme.Dialogue.AdvanceDialogue.triggered)
        {
            DialogueManager.GetDialogueManager().AdvanceDialogue();
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

    private Door lastInteractedDoor;
    private bool justTraveled = false;
    private bool _isTeleporting;

    /// <summary>
    /// Runs when player enters a trigger.
    /// <param name="collision"></param>
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            Door tempDoor = collision.gameObject.GetComponent<Door>();
            // Active doors require interaction whereas passive doors do not.
            if (!tempDoor.IsActive())
            {
                if (lastInteractedDoor == null && justTraveled == false)
                {
                    if (SceneManager.GetActiveScene().name == tempDoor.GetDestinationSceneName())
                    {
                        _isTeleporting = true;
                        transform.position = tempDoor.GetDestinationLocation();
                        Door[] doorObjects = FindObjectsByType<Door>(FindObjectsSortMode.None);
                        foreach (Door door in doorObjects)
                        {
                            if (door.transform.position == transform.position)
                            {
                                lastInteractedDoor = door;
                                break;
                            }
                        }
                        Invoke(nameof(UnlockTeleport), .5f);
                    }
                    else
                    {
                        _isTeleporting = true;
                        SceneManager.LoadScene(tempDoor.GetDestinationSceneName());
                        transform.position = tempDoor.GetDestinationLocation();
                        Door[] doorObjects = FindObjectsByType<Door>(FindObjectsSortMode.None);
                        foreach (Door door in doorObjects)
                        {
                            if (math.abs(door.transform.position.x - transform.position.x) < 1 && math.abs(door.transform.position.y - transform.position.y) < 1)
                            {
                                lastInteractedDoor = door;
                                break;
                            }
                        }
                        Invoke(nameof(UnlockTeleport), .5f);
                    }

                    justTraveled = true;
                }
            }
        }
    }

    /// <summary>
    /// Runs when player exits a trigger.
    /// <param name="collision"></param>
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isTeleporting) { return; }
        if (lastInteractedDoor == null || (collision.gameObject.CompareTag("Door") && lastInteractedDoor.gameObject == collision.gameObject))
        {
            justTraveled = false;
            lastInteractedDoor = null;
        }
    }

    /// <summary>
    /// allows the player to use doors again
    /// </summary>
    private void UnlockTeleport()
    {
        _isTeleporting = false;
    }

    /// <summary>
    /// When the player presses 'E', check for an interactable object in the facing direction. 
    /// </summary>
    private void Interact()
    {
        var facingDir = new Vector3(characterAnimator.GetFloat("moveX"), characterAnimator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, _interactableObjectsLayer);
        if (collider != null)
        {
            if (collider.TryGetComponent<QuestHandler>(out QuestHandler qh))
            {
                int handledQuestId = ((IQuestOwner)this).GetHandledQuestId(qh);
                if (handledQuestId != -1 && ((IQuestOwner)this).IsQuestCompleteable(handledQuestId))
                {
                    QuestFramework.GetQuestFramework().CompleteQuest(handledQuestId, qh, (IQuestOwner)this);
                }

                else
                {
                    collider.GetComponent<InteractableObject>()?.Interact();
                }
            }
            else
            {
                collider.GetComponent<InteractableObject>()?.Interact();
            }

        }
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
        displayMngr.SetGold(_amountOfGold);
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

    public bool QueryInventory(ItemId itemId, int qty = 1)
    {
        bool isItemPresent = _playerInventory.InventoryContents.TryGetValue(itemId, out int amtInInventory);
        return isItemPresent && qty <= amtInInventory;
    }

    public void AddGold(int amtToAdd)
    {
        _amountOfGold += amtToAdd;
        if (_inventoryManager != null)
        {
            _inventoryManager.SetGold(_amountOfGold);
        }
    }

    public PlayerInventory GetInventory()
    {
        return _playerInventory;
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

    #region Quests
    /// <summary>
    /// Instantiates the quest menu as a child of player and returns a reference to the manager script
    /// </summary>
    /// <returns>QuestMenuManager</returns>
    private QuestMenuManager CreateQuestMenu()
    {
        GameObject display = Instantiate(_questMenuPrefab, gameObject.transform);
        display.SetActive(false);
        QuestMenuManager qMenuMngr = display.GetComponent<QuestMenuManager>();
        qMenuMngr.SetQuestList(OwnedQuests);
        return qMenuMngr;
    }

    /// <summary>
    /// Activates/Deactivates the quest menu screen to display/hide it
    /// </summary>
    private void ToggleQuestMenu()
    {
        if (_questMenuManager == null)
        {
            _questMenuManager = CreateQuestMenu();
            _questMenuManager.QuestMenuClose += OnQuestMenuClose;
        }

        _questMenuManager.ToggleQuestMenu();
    }

    /// <summary>
    /// Handles the QuestMenuClose event - necessary to allow closing from quest menu X button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void OnQuestMenuClose(object sender, EventArgs e)
    {
        ToggleQuestMenu();
    }
    #endregion

    #region Controls
    /// <summary>
    /// Turns on all controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ActivateStandardControls(object sender, EventArgs e)
    {
        _controlScheme.Dialogue.Disable();
        _controlScheme.Standard.Enable();
    }
    /// <summary>
    /// Turns off controls not necessary for dialogue
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ActivateDialogueControls(object sender, EventArgs e)
    {
        _controlScheme.Standard.Disable();
        _controlScheme.Dialogue.Enable();
    }
    #endregion

    #region TeleportationControls
    public void StartTeleportation()
    {
        _controlScheme.Standard.Disable();
    }

    public void EndTeleportation()
    {
        _controlScheme.Standard.Enable();
    }
    #endregion
}

