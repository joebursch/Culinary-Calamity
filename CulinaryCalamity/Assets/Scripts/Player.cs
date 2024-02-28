using System;
using System.Collections.Generic;
using Saving;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private List<Item> playerInventory; // Update to Inventory reference
    [SerializeField] private int amountOfGold;
    private Questline questline;
    private Actions controlScheme = null;
    private Vector2 movementDir;
    [SerializeField] private LayerMask solidObjectsLayer;
    private ObjectSaveData playerSaveData;

    void Awake()
    {
        controlScheme = new Actions();
        playerSaveData = new();
    }

    void Start()
    {
        GameSaveManager.GetGameSaveManager().Save += OnSave;
        GameSaveManager.GetGameSaveManager().Load += OnLoad;
    }
    void OnEnable() => controlScheme.Standard.Enable();

    void OnDestroy() => controlScheme.Standard.Disable();

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
        // Get vector values for movement
        movementDir = controlScheme.Standard.Move.ReadValue<Vector2>();
        // Limit movement to up/down/left/right (No diagonal movement)
        if (movementDir.x > 0 | movementDir.x < 0) { movementDir.y = 0; }
        // Check if the target position is walkable
        var targetPos = transform.position;
        targetPos.x += movementDir.x;
        targetPos.y += movementDir.y;
        if (IsWalkable(targetPos))
        {
            transform.Translate(movementDir * movementSpeed * Time.deltaTime);
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    // Player movement 
    void Update()
    {
        MovePlayer();
    }
}
