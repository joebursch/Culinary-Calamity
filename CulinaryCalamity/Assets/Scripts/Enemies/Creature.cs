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

        #region Attributes
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
        #endregion

        #region UnityBuiltIn

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

        #region Protected Management Functions
        protected void InitializeCreature()
        {
            _currentCreatureState = (int)CREATURE_STATE.Roaming;
            characterAnimator = GetComponent<Animator>();
            _spawnPosition = transform.position;
            SetMovementDirection(); // Need to set wander direction off start so the creature moves
        }
        protected void ManageCreatureState()
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
        #endregion

        #region Private Helper Functions
        private void CheckDistanceFromSpawner()
        {
            var distanceOnXAxis = Mathf.Abs(transform.position.x - _spawnPosition.x);
            var distanceOnYAxis = Mathf.Abs(transform.position.y - _spawnPosition.y);
            if (distanceOnXAxis > _maxDistanceFromSpawn | distanceOnYAxis > _maxDistanceFromSpawn)
            {
                _currentCreatureState = (int)CREATURE_STATE.Retreating;
            }
        }

        private void SetMovementDirection()
        {
            switch (_currentCreatureState)
            {
                case (int)CREATURE_STATE.Roaming:
                    _movementDir = GetWanderDirection();
                    break;
                case (int)CREATURE_STATE.Hunting:
                    _movementDir.x = _huntingTarget.transform.position.x - transform.position.x;
                    _movementDir.y = _huntingTarget.transform.position.y - transform.position.y;
                    _movementDir.Normalize();
                    break;
                case (int)CREATURE_STATE.Retreating:
                    _movementDir.x = _spawnPosition.x - transform.position.x;
                    _movementDir.y = _spawnPosition.y - transform.position.y;
                    _movementDir.Normalize();
                    break;
            }

        }

        private Vector2 GetWanderDirection()
        {
            Vector2 wanderVector = new Vector2();
            var wanderDirection = Random.Range(0, 4);
            switch (wanderDirection)
            {
                case 0:
                    wanderVector = Vector2.up;
                    break;
                case 1:
                    wanderVector = Vector2.down;
                    break;
                case 2:
                    wanderVector = Vector2.left;
                    break;
                case 3:
                    wanderVector = Vector2.right;
                    break;
            }
            return wanderVector;
        }
        #endregion

        #region Creature States
        private void Wander()
        {
            if (_currentRoamTime > _creatureMaxRoamTimer)
            {
                SetMovementDirection();
                _currentRoamTime = 0;
            }
            ConfigureAnimator(_movementDir, false);
            if (IsWalkable(_movementDir))
            {
                transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
            }
            else { SetMovementDirection(); }
        }

        private void Hunt()
        {
            SetMovementDirection();
            ConfigureAnimator(_movementDir, true);
            transform.Translate(_movementDir * _creatureRunSpeed * Time.deltaTime);
        }

        private void Retreat()
        {
            if (Vector3.Distance(transform.position, _spawnPosition) < 0.2f) { _currentCreatureState = (int)CREATURE_STATE.Roaming; return; }
            SetMovementDirection();
            ConfigureAnimator(_movementDir, false);
            transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
        }
        #endregion
    }
}
