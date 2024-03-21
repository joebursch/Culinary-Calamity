using System.Collections.Generic;
using UnityEngine;
using Items;
using Attacks;

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
        [SerializeField] private float _creatureAttackRange;
        [SerializeField] private int damage;
        [SerializeField] private int _itemIdToDrop;
        [SerializeField] private int _maxDistanceFromSpawn;
        [SerializeField] protected float _timeBetweenAttacks;
        private int _currentCreatureState;
        private Vector2 _movementDir = Vector2.zero;
        private Vector3 _spawnPosition;
        private GameObject _huntingTarget;
        private float _currentWanderTime;
        private float _timeSinceLastAttack;
        private AttackStrategy _attackStrategy;
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
        protected void InitializeCreature(AttackStrategy attackStrategy)
        {
            currentHealth = characterHealth;
            _attackStrategy = attackStrategy;
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
        /// <summary>
        /// Check if the creature is within its specified attack range. 
        /// </summary>
        /// <returns>Boolean</returns>
        private bool InAttackRange()
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, _huntingTarget.transform.position)) > _creatureAttackRange)
            {
                return false;
            }
            return true;
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
            if (InAttackRange())
            {
                if (_attackStrategy.CanAttack(_timeSinceLastAttack))
                {
                    _attackStrategy.Attack(_huntingTarget.transform.position);
                    _timeSinceLastAttack = 0;
                }
                else { _timeSinceLastAttack += Time.deltaTime; }
            }
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

        /// <summary>
        /// Method for dealing damage to a creature.
        /// </summary>
        /// <param name="damage">Amount of damage dealt.</param>
        public void TakeDamage(float damage)
        {
            SetCurrentHealth(-damage);
            // Knockback effect
            transform.position = new Vector3(transform.position.x - (_movementDir.x * 2), transform.position.y - (_movementDir.y * 2), transform.position.z);
            if (currentHealth <= 0) { Death(); }
        }
        /// <summary>
        /// Method for a creatures death...
        /// </summary>
        void Death()
        {
            // 25% chance to drop an item
            int randomNum = Random.Range(0, 4);
            if (randomNum == 2)
            {
                ItemManager.GetItemManager().SpawnItem((ItemId)_itemIdToDrop, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        #endregion
    }
}
