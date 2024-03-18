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

    private int _currentCreatureState;

    private Vector2 _movementDir = Vector2.zero;

    private float _currentRoamTime;

    #region UnityBuiltIn
    void Awake()
    {
        _currentCreatureState = (int)CREATURE_STATE.Roaming;
        characterAnimator = GetComponent<Animator>();
        SetWanderDirection(); // Need to set wander direction off start so the creature moves
    }


    void Update()
    {
        _currentRoamTime += Time.deltaTime;
        switch (_currentCreatureState)
        {
            case (int)CREATURE_STATE.Hunting:
                Debug.Log("Creature Hunting");
                break;
            case (int)CREATURE_STATE.Retreating:
                Debug.Log("Creature Retreating");
                break;
            default:
                Wander();
                break;
        }
    }
    #endregion


    #region Movement
    void Wander()
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

    void SetWanderDirection()
    {
        var wanderDirection = Random.Range(0, 4);
        switch (wanderDirection)
        {
            case 0:
                _movementDir = Vector2.up;
                break;
            case 1:
                //down
                _movementDir = Vector2.down;
                break;
            case 2:
                //left
                _movementDir = Vector2.left;
                break;
            case 3:
                //right
                _movementDir = Vector2.right;
                break;
        }
    }
    #endregion
}
