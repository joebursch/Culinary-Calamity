using System.Collections.Generic;
using UnityEngine;
using Items;

public class Creature : Character
{

    enum CREATURE_STATE : int
    {
        Roaming = 0,
        Hunting = 1,
        Retreating = 2,
    }


    [SerializeField] private int _creatureWalkSpeed;
    [SerializeField] private int _creatureRunSpeed;

    [SerializeField] private int _creatureMaxRoamTimer;

    [SerializeField] private int damage;
    [SerializeField] private Item dropItem;

    [SerializeField] private int _maxDistanceFromSpawn;



    private int _currentCreatureState;

    private Vector2 _movementDir = Vector2.zero;
    private Vector3 _spawnPosition;
    private GameObject _huntingTarget;

    private float _currentRoamTime;

    #region UnityBuiltIn
    void Awake()
    {
        _currentCreatureState = (int)CREATURE_STATE.Roaming;
        characterAnimator = GetComponent<Animator>();
        _spawnPosition = transform.position;
        SetWanderDirection(); // Need to set wander direction off start so the creature moves
    }


    void Update()
    {
        _currentRoamTime += Time.deltaTime;
        switch (_currentCreatureState)
        {
            case (int)CREATURE_STATE.Hunting:
                Hunt();
                break;
            case (int)CREATURE_STATE.Retreating:
                Retreat();
                break;
            default:
                CheckDistanceFromSpawner();
                Wander();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        _currentCreatureState = (int)CREATURE_STATE.Hunting;
        _huntingTarget = collider2D.gameObject;
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        _huntingTarget = null;
        _currentCreatureState = (int)CREATURE_STATE.Retreating;
    }


    #endregion

    private void CheckDistanceFromSpawner()
    {
        var distanceOnXAxis = Mathf.Abs(transform.position.x - _spawnPosition.x);
        var distanceOnYAxis = Mathf.Abs(transform.position.y - _spawnPosition.y);
        if (distanceOnXAxis > _maxDistanceFromSpawn | distanceOnYAxis > _maxDistanceFromSpawn)
        {
            _currentCreatureState = (int)CREATURE_STATE.Retreating;
        }
    }

    #region Roaming
    private void Wander()
    {
        if (_currentRoamTime > _creatureMaxRoamTimer)
        {
            SetWanderDirection();
            _currentRoamTime = 0;
        }
        ConfigureAnimator(_movementDir, false);
        if (IsWalkable(_movementDir))
        {
            transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
        }
        else { SetWanderDirection(); }
    }

    private void SetWanderDirection()
    {
        var wanderDirection = Random.Range(0, 4);
        switch (wanderDirection)
        {
            case 0:
                _movementDir = Vector2.up;
                break;
            case 1:
                _movementDir = Vector2.down;
                break;
            case 2:
                _movementDir = Vector2.left;
                break;
            case 3:
                _movementDir = Vector2.right;
                break;
        }
    }
    #endregion

    #region Hunting
    private void Hunt()
    {
        SetFacingDirection(_huntingTarget.transform.position);
        ConfigureAnimator(_movementDir, true);
        var step = _creatureRunSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _huntingTarget.transform.position, step);
    }
    #endregion

    #region Retreating
    private void Retreat()
    {
        if ((transform.position - _spawnPosition) == Vector3.zero) { _currentCreatureState = (int)CREATURE_STATE.Roaming; }
        SetFacingDirection(_spawnPosition);
        ConfigureAnimator(_movementDir, false);
        var step = _creatureWalkSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _spawnPosition, step);
    }
    #endregion

    private void SetFacingDirection(Vector3 targetPosition)
    {
        var diffX = transform.position.x - targetPosition.x;
        var diffY = transform.position.y - targetPosition.y;
        if (Mathf.Abs(diffX) < 0.05f)
        {
            if (diffY < 0)
            {
                _movementDir = Vector2.up;
            }
            else { _movementDir = Vector2.down; }
        }
        else
        {
            if (diffX < 0)
            {
                _movementDir = Vector2.right;
            }
            else { _movementDir = Vector2.left; }
        }

    }
}
