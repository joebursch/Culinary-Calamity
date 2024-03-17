using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// NPC class.
/// </summary>
public class NPC : Character, InteractableObject
{

    // movement
    private Vector2 _movementDir = Vector2.zero;
    private Vector2 _nextMoveDir;
    private float _lastMoveTime;
    private bool _walkingBackNext = false;
    [SerializeField] private bool wanderAroundSpawnpoint;
    // layers
    [SerializeField] private LayerMask _solidObjectsLayer;
    [SerializeField] private LayerMask _itemsLayer;
    [SerializeField] private LayerMask _defaultLayer;

    /// <summary>
    /// Called on script load.
    /// </summary>
    void Awake()
    {
        movementSpeed = 5;
        characterAnimator = GetComponent<Animator>();
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

        ConfigureAnimator();
        if (IsWalkable())
        {
            transform.Translate(movementSpeed * Time.deltaTime * _movementDir);
        }
    }

    /// <summary>
    /// Method to move an NPC if MoveNPC is not passed anything.
    /// </summary>
    void MoveNPC()
    {
        ConfigureAnimator();
        if (IsWalkable())
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
        if (Time.time > _lastMoveTime + 3 & Time.time <= _lastMoveTime + 10)
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
    private void PickWalkDirection(bool setNextMove=false)
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
    /// Handle the animator.
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
        characterAnimator.SetBool("isWalking", moving);
    }

    /// <summary>
    /// Check if NPC can move to a location.
    /// </summary>
    private bool IsWalkable()
    {
        var targetPos = transform.position;
        targetPos.x += _movementDir.x;
        targetPos.y += _movementDir.y;
        if (Physics2D.OverlapCircle(targetPos, 0.2f, _solidObjectsLayer | _defaultLayer) != null)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Did the NPC interact with something?
    /// </summary>
    void InteractableObject.Interact()
    {
        Debug.Log("Touched!");
    }
}