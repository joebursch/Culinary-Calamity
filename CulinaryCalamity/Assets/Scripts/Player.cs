using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Player : Character
{
    [SerializeField] private Inventory _playerInventory; // Update to Inventory reference
    [SerializeField] private int _amountOfGold;
    private Questline _questline;
    private Actions _controlScheme = null;
    private Vector2 _movementDir;
    private float _Running;
    [SerializeField] private LayerMask _solidObjectsLayer;

    void Awake()
    {
        _controlScheme = new Actions();
        characterAnimator = GetComponent<Animator>();
    }

    void OnEnable() => _controlScheme.Standard.Enable();

    void OnDestroy() => _controlScheme.Standard.Disable();

    void movePlayer()
    {
        // Get vector values for movement
        _movementDir = _controlScheme.Standard.Move.ReadValue<Vector2>();
        _Running = _controlScheme.Standard.Run.ReadValue<float>();
        // Limit movement to up/down/left/right (No diagonal movement)
        if (_movementDir.x > 0 | _movementDir.x < 0) { _movementDir.y = 0; }
        if (_movementDir != Vector2.zero)
        {
            characterAnimator.SetFloat("moveX", _movementDir.x);
            characterAnimator.SetFloat("moveY", _movementDir.y);

            if (_Running == 1)
            {
                movementSpeed = 10;
                characterAnimator.SetBool("isRunning", true);
            }
            else
            {
                movementSpeed = 3;
                characterAnimator.SetBool("isRunning", false);
                characterAnimator.SetBool("isWalking", true);
            }
        }
        else
        {
            characterAnimator.SetBool("isWalking", false);
            characterAnimator.SetBool("isRunning", false);
        }
        // Check if the target position is walkable
        if (IsWalkable())
        {
            transform.Translate(_movementDir * movementSpeed * Time.deltaTime);
        }
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
