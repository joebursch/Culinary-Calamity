using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private Inventory _playerInventory; // Update to Inventory reference
    [SerializeField] private int _amountOfGold;
    private Questline _questline;
    private Actions _controlScheme = null;
    private Vector2 _movementDir;
    private bool _running;
    [SerializeField] private LayerMask _solidObjectsLayer;

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
    }

    void OnEnable() => _controlScheme.Standard.Enable();

    void OnDestroy() => _controlScheme.Standard.Disable();

    void movePlayer()
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
        movePlayer();
    }
}
