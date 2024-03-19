using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Enemies
{
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

        private void SetMovementDirection(Vector3 target)
        {
            _movementDir.x = target.x - transform.position.x;
            _movementDir.y = target.y - transform.position.y;
            _movementDir.Normalize();
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
            SetMovementDirection(_huntingTarget.transform.position);
            ConfigureAnimator(_movementDir, true);
            transform.Translate(_movementDir * _creatureRunSpeed * Time.deltaTime);
        }
        #endregion

        #region Retreating
        private void Retreat()
        {
            if (Vector3.Distance(transform.position, _spawnPosition) < 0.2f) { _currentCreatureState = (int)CREATURE_STATE.Roaming; return; }
            SetMovementDirection(_spawnPosition);
            ConfigureAnimator(_movementDir, false);
            transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
        }
        #endregion
    }
}
