using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Attacks
{
    public class MeleeAttack : AttackStrategy
    {

        private float _damageToDeal;
        private float _attackerAttackTime;
        private LayerMask _layerToAttack;


        /// <summary>
        /// Constructor for a Melee Attack
        /// </summary>
        /// <param name="damage">Amount of damage to deal per attack</param>
        /// <param name="attackLayer">The layer on which we want to register attacks</param>
        public MeleeAttack(float damage, LayerMask attackLayer)
        {
            _damageToDeal = damage;
            _layerToAttack = attackLayer;
        }
        /// <summary>
        /// Contstructor for timed Melee attacks
        /// </summary>
        /// <param name="damage">Amount of damage per attack</param>
        /// <param name="attackLayer">Layer to register attacks on</param>
        /// <param name="timeBetweenAttacks">Delay between attacks</param>
        public MeleeAttack(float damage, LayerMask attackLayer, float timeBetweenAttacks)
        {
            _damageToDeal = damage;
            _layerToAttack = attackLayer;
            _attackerAttackTime = timeBetweenAttacks;
        }

        /// <summary>
        /// Method for triggering an attack.
        /// </summary>
        /// <param name="targetPosition">Position of the attack victim</param>
        public void Attack(Vector3 targetPosition)
        {
            Melee(targetPosition);
        }

        /// <summary>
        /// Determines if the attacker is able to attack
        /// </summary>
        /// <param name="timeSinceLastAttack">Amount of time since last attack.</param>
        /// <returns></returns>
        public bool CanAttack(float timeSinceLastAttack)
        {
            if (timeSinceLastAttack < _attackerAttackTime)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Carries out a melee attack.
        /// </summary>
        /// <param name="targetPosition">Position of melee victim</param>
        private void Melee(Vector3 targetPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(targetPosition.x, targetPosition.y), 1f, _layerToAttack);
            foreach (Collider2D collider2D in colliders)
            {
                if (!collider2D.isTrigger)
                {
                    collider2D.gameObject.GetComponent<Character>().TakeDamage(_damageToDeal);
                }
            }
        }
    }
}

