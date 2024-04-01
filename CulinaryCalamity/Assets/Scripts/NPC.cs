using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Dialogue;

/// <summary>
/// NPC class.
/// </summary>
public class NPC : Character, InteractableObject
{
    // movement
    private Vector2 _nextMoveDir;
    private float _lastMoveTime;
    private bool _walkingBackNext = false;
    [SerializeField] private bool wanderAroundSpawnpoint;
    [SerializeField] private TextAsset _dialogue = null;
    private DialogueManager _dialogueManager = null;

    /// <summary>
    /// Called on script load.
    /// </summary>
    void Awake()
    {
        movementSpeed = 5;
        characterAnimator = GetComponent<Animator>();
        if (_dialogue != null)
        {
            _dialogueManager = new();
            _dialogueManager.InitializeDialogue(_dialogue);
        }
    }

    /// <summary>
    /// Called on script enabled.
    /// </summary>
    void Start()
    {
        _lastMoveTime = Time.time;
    }

    /// <summary>
    /// Called every tick.
    /// </summary>
    void Update()
    {
        MoveNPC();
    }

    /// <summary>
    /// Method to set the NPC's direction if MoveNPC is sent a Vector2.
    /// </summary>
#pragma warning disable IDE0051
    void MoveNPC(Vector2 moveTo)
#pragma warning restore IDE0051
    {
        _movementDir = moveTo;

        ConfigureAnimator(_movementDir);
        if (IsWalkable(_movementDir))
        {
            transform.Translate(movementSpeed * Time.deltaTime * _movementDir);
        }
    }

    /// <summary>
    /// Method to move an NPC if MoveNPC is not passed anything.
    /// </summary>
    void MoveNPC()
    {
        ConfigureAnimator(_movementDir);

        if (IsWalkable(_movementDir))
        {
            transform.Translate(movementSpeed * Time.deltaTime * _movementDir);
        }

        if (wanderAroundSpawnpoint)
        {
            Wander();
        }
    }

    /// <summary>
    /// Method to handle wandering function of NPCs.
    /// </summary>
    private void Wander()
    {
        if (Time.time > _lastMoveTime + 3 && Time.time <= _lastMoveTime + 10)
        {
            _movementDir = Vector2.zero;
        }
        else if (Time.time > _lastMoveTime + 10)
        {
            if (_walkingBackNext == false)
            {
                PickWalkDirection(true);
                _lastMoveTime = Time.time;
                _walkingBackNext = true;
            }
            else
            {
                _movementDir = _nextMoveDir;
                _lastMoveTime = Time.time;
                _walkingBackNext = false;
            }
        }
    }

    /// <summary>
    /// Method to randomly pick the next walk direction for Wander().
    /// </summary>
    private void PickWalkDirection(bool setNextMove = false)
    {
        switch (UnityEngine.Random.Range(1, 5))
        {
            case 1:
                _movementDir = Vector2.up;
                if (setNextMove) { _nextMoveDir = Vector2.down; }
                break;
            case 2:
                _movementDir = Vector2.down;
                if (setNextMove) { _nextMoveDir = Vector2.up; }
                break;
            case 3:
                _movementDir = Vector2.left;
                if (setNextMove) { _nextMoveDir = Vector2.right; }
                break;
            case 4:
                _movementDir = Vector2.right;
                if (setNextMove) { _nextMoveDir = Vector2.left; }
                break;
        }
    }

    /// <summary>
    /// Did the NPC interact with something?
    /// </summary>
    public void Interact()
    {

        if (_dialogueManager != null && _dialogueManager.StillSpeaking())
        {
            _dialogueManager.PlayLine();
        }
        Debug.Log("Touched!");
    }
    /// <summary>
    /// Method for NPC taking damage... spoiler -> they dont. 
    /// </summary>
    /// <param name="damage">Damage to not deal</param>
    public override void TakeDamage(float damage)
    {
        // Do nothing -> NPCS are tanks
    }
    /// <summary>
    /// Method for NPC death -> they are immortal tbh
    /// </summary>
    protected override void Death()
    {
        // Do nothing -> NPCs can't die
    }
}