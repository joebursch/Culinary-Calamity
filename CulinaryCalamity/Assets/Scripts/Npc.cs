using System;
using UnityEngine;

public class NPC : Character, InteractableObject
{
    enum NPC_SPD : int
    {
        Walk = 5,
        Run = 10,
    }

    // movement
    private Vector2 _movementDir;
    private float _lastMoveTime;
    private bool _running;
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
        _movementDir = Vector2.zero;

        if (Time.time > _lastMoveTime + 8)
        {
            switch (UnityEngine.Random.Range(1, 5))
            {
                case 1:
                    _movementDir = Vector2.up;
                    break;
                case 2:
                    _movementDir = Vector2.down;
                    break;
                case 3:
                    _movementDir = Vector2.right;
                    break;
                case 4:
                    _movementDir = Vector2.up;
                    break;
            }
            _lastMoveTime = Time.time;
        }

        CheckRunning();
        ConfigureAnimator();
        if (IsWalkable())
        {
            transform.Translate(_movementDir * movementSpeed * Time.deltaTime);
        }
    }

    private void CheckRunning()
    {
        if (movementSpeed == (int)NPC_SPD.Run)
        {
            _running = !_running;
            switch (movementSpeed)
            {
                case (int)NPC_SPD.Walk:
                    movementSpeed = (int)NPC_SPD.Run;
                    break;
                default:
                    movementSpeed = (int)NPC_SPD.Walk;
                    break;
            }
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
        characterAnimator.SetBool("isRunning", _running && moving);
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