using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : Character, InteractableObject
{
    enum NPC_SPD : int
    {
        Walk = 5,
    }

    // movement
    private Vector2 _movementDir = Vector2.zero;
    private Vector2 _nextMoveDir;
    private float _lastMoveTime = Time.time;
    private bool _walkingBackNext = false;
    [SerializeField] private bool wanderAroundSpawnpoint;
    // layers
    [SerializeField] private LayerMask _solidObjectsLayer;
    [SerializeField] private LayerMask _interactableObjectsLayer;
    [SerializeField] private LayerMask _itemsLayer;

    void Awake()
    {
        movementSpeed = (int)NPC_SPD.Walk;
        characterAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        _lastMoveTime = Time.time;
    }

    void Update()
    {
        MoveNPC();
    }

    void MoveNPC()
    {
        ConfigureAnimator();
        if (IsWalkable())
        {
            transform.Translate(movementSpeed * Time.deltaTime * _movementDir);
        }


        if (wanderAroundSpawnpoint)
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
    }

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

    void InteractableObject.Interact()
    {
        Debug.Log("Touched!");
    }
}