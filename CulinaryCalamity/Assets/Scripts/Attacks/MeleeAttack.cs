using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Attacks
{
    public class MeleeAttack : AttackStrategy
    {

        private float _damageToDeal;
        private LayerMask _layerToAttack;

        public MeleeAttack(float damage, LayerMask attackLayer)
        {
            _damageToDeal = damage;
            _layerToAttack = attackLayer;
        }


        public void Attack(Vector3 targetPosition)
        {
            Melee(targetPosition);
        }

        public bool CanAttack(float timeSinceLastAttack)
        {
            // Implement Later
            /*
                Player -> Maybe we want them to attack as often as they want (ie. as fast as they can click)
                Creature -> Maybe we want a limit on creature attacks so we don't die instantly
            */
            return false;
        }

        private void Melee(Vector3 targetPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(targetPosition.x, targetPosition.y), 1f, _layerToAttack);
            Debug.Log(colliders.Length);
            foreach (Collider2D collider2D in colliders)
            {
                collider2D.gameObject.GetComponent<Creature>().TakeDamage(_damageToDeal);
            }
        }
    }
}

