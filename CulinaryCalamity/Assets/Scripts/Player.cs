using System;
using System.Collections.Generic;
using Saving;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Inventory _playerInventory; // Update to Inventory reference
    [SerializeField] private int _amountOfGold;
    private Questline _questline;
    private Actions _controlScheme = null;
    private Vector2 _movementDir;
    private bool _running;
    [SerializeField] private LayerMask _solidObjectsLayer;
    [SerializeField] private LayerMask _interactableObjectsLayer;
    private ObjectSaveData playerSaveData;

    enum PLAYER_STATS : int
    {
        Walk = 5,
        Run = 10,
    }

    void Awake()
    {
        movementSpeed = (int)PLAYER_STATS.Walk;
        _controlScheme = new Actions();
        characterAnimator = GetComponent<Animator>();
        playerSaveData = new();
    }

    void Start()
    {
        GameSaveManager.GetGameSaveManager().Save += OnSave;
        GameSaveManager.GetGameSaveManager().Load += OnLoad;
    }

    void OnEnable() => _controlScheme.Standard.Enable();

    void OnDestroy() => _controlScheme.Standard.Disable();

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
                case (int)PLAYER_STATS.Walk:
                    movementSpeed = (int)PLAYER_STATS.Run;
                    break;
                default:
                    movementSpeed = (int)PLAYER_STATS.Walk;
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
        if (CheckCollision(targetPos, _interactableObjectsLayer | _solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void Interact()
    {
        var facingDir = new Vector3(characterAnimator.GetFloat("moveX"), characterAnimator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDir;
        var collider = CheckCollision(interactPosition, _interactableObjectsLayer);
        if (collider != null) { collider.GetComponent<InteractableObject>()?.Interact(); }
    }

    private Collider2D CheckCollision(Vector3 targetPos, LayerMask targetLayer)
    {
        //Debug.Log("Checking Collision!");
        return Physics2D.OverlapCircle(targetPos, 0.2f, targetLayer);
    }

    // Player update loop
    void Update()
    {
        MovePlayer();
        if (_controlScheme.Standard.Interact.triggered) { Interact(); }
    }
}
