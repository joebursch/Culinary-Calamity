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

    private Vector2 GetMovementDirection()
    {
        var tempDir = _controlScheme.Standard.Move.ReadValue<Vector2>();
        if (tempDir.x > 0 | tempDir.x < 0) { tempDir.y = 0; }
        return tempDir;
    }

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

    private void ConfigureAnimator()
    {
        bool moving = _movementDir != Vector2.zero;
        characterAnimator.SetFloat("moveX", _movementDir.x);
        characterAnimator.SetFloat("moveY", _movementDir.y);
        characterAnimator.SetBool("isRunning", _running && moving);
        characterAnimator.SetBool("isWalking", moving);
    }

    private bool IsWalkable()
    {
        var targetPos = transform.position;
        targetPos.x += _movementDir.x;
        targetPos.y += _movementDir.y;
        if (Physics2D.OverlapCircle(targetPos, 0.2f, _solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    // Player update loop
    void Update()
    {
        MovePlayer();
    }
}
