using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Enemies
{
    public class Creature : Character
    {

        // Represents possible states of a creature
        enum CREATURE_STATE : int
        {
            Wandering = 0,
            Hunting = 1,
            Retreating = 2,
        }

        #region Attributes
        [SerializeField] private int _creatureWalkSpeed;
        [SerializeField] private int _creatureRunSpeed;
        [SerializeField] private int _creatureMaxWanderTime;
        [SerializeField] private int damage;
        [SerializeField] private Item dropItem;
        [SerializeField] private int _maxDistanceFromSpawn;

        private int _currentCreatureState;
        private Vector2 _movementDir = Vector2.zero;
        private Vector3 _spawnPosition;
        private GameObject _huntingTarget;
        private float _currentWanderTime;
        #endregion

        #region UnityBuiltIn

        void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.name == "Player")
            {
                _currentCreatureState = (int)CREATURE_STATE.Hunting;
                _huntingTarget = collider2D.gameObject;
            }

        }
        void OnTriggerExit2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.name == "Player")
            {
                _huntingTarget = null;
                _currentCreatureState = (int)CREATURE_STATE.Retreating;
            }
        }

        #endregion

        #region Protected Management Functions

        /// <summary>
        /// Function that sets up the creature and it's states
        /// </summary>
        protected void InitializeCreature()
        {
            _currentCreatureState = (int)CREATURE_STATE.Wandering;
            characterAnimator = GetComponent<Animator>();
            _spawnPosition = transform.position;
            SetMovementDirection(); // Need to set wander direction off start so the creature moves
        }
        /// <summary>
        /// Manages creature behavior based on the creature's state
        /// </summary>
        protected void ManageCreatureState()
        {
            _currentWanderTime += Time.deltaTime;
            switch (_currentCreatureState)
            {
                case (int)CREATURE_STATE.Hunting:
                    Hunt();
                    break;
                case (int)CREATURE_STATE.Retreating:
                    Retreat();
                    break;
                case (int)CREATURE_STATE.Wandering:
                    CheckDistanceFromSpawner();
                    Wander();
                    break;
            }
        }
        #endregion

        #region Private Helper Functions
        /// <summary>
        /// Checks to see if the creature is too far from the spawner.
        /// Creature should return to the spawner if they have gone too far away. 
        /// </summary>
        private void CheckDistanceFromSpawner()
        {
            var distanceOnXAxis = Mathf.Abs(transform.position.x - _spawnPosition.x);
            var distanceOnYAxis = Mathf.Abs(transform.position.y - _spawnPosition.y);
            if (distanceOnXAxis > _maxDistanceFromSpawn | distanceOnYAxis > _maxDistanceFromSpawn)
            {
                _currentCreatureState = (int)CREATURE_STATE.Retreating;
            }
        }
        /// <summary>
        /// Sets movement vector based on creature state. 
        /// </summary>
        private void SetMovementDirection()
        {
            switch (_currentCreatureState)
            {
                case (int)CREATURE_STATE.Wandering:
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
        /// <summary>
        /// Gets a random direction for wandering when the creature is in the wandering state. 
        /// </summary>
        /// <returns>Vector2</returns>
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
        /// <summary>
        /// Method for behaviour when a creature is wandering. 
        /// </summary>
        private void Wander()
        {
            if (_currentWanderTime > _creatureMaxWanderTime)
            {
                SetMovementDirection();
                _currentWanderTime = 0;
            }
            ConfigureAnimator(_movementDir, false);
            if (IsWalkable(_movementDir))
            {
                transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
            }
            else { SetMovementDirection(); }
        }
        /// <summary>
        /// Method for behaviour when a creature is hunting the player. 
        /// </summary>
        private void Hunt()
        {
            SetMovementDirection();
            ConfigureAnimator(_movementDir, true);
            if (IsWalkable(_movementDir))
            {
                transform.Translate(_movementDir * _creatureRunSpeed * Time.deltaTime);
            }
        }
        /// <summary>
        /// Method for behaviour when a creature is returning to its spawn point. 
        /// </summary>
        private void Retreat()
        {
            if (Vector3.Distance(transform.position, _spawnPosition) < 0.2f) { _currentCreatureState = (int)CREATURE_STATE.Wandering; return; }
            SetMovementDirection();
            ConfigureAnimator(_movementDir, false);
            transform.Translate(_movementDir * _creatureWalkSpeed * Time.deltaTime);
        }
        #endregion
    }
}
