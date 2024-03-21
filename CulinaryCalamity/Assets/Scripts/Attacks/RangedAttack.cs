using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class RangedAttack : AttackStrategy
    {
        private GameObject _projectileToShoot;
        private Transform _projectileShooter;
        private float _shooterAttackTime;
        /// <summary>
        /// Constructor for a Ranged Attack.
        /// </summary>
        /// <param name="projectile">Projectile to be shot</param>
        /// <param name="shooter">Character shooting the projectile</param>
        /// <param name="timeBetweenAttacks">Delay between attacks</param>
        public RangedAttack(GameObject projectile, Transform shooter, float timeBetweenAttacks)
        {
            _projectileToShoot = projectile;
            _projectileShooter = shooter;
            _shooterAttackTime = timeBetweenAttacks;
        }
        /// <summary>
        /// Method for triggering an attack.
        /// </summary>
        /// <param name="targetPosition">Position of the attack victim</param>
        public void Attack(Vector3 targetPosition)
        {
            ShootProjectile().GetComponent<Projectile>().SetTargetPosition(targetPosition);
        }

        /// <summary>
        /// Creates the projectile belonging to the attacking character.
        /// </summary>
        /// <returns>GameObject of projectile</returns>
        private GameObject ShootProjectile()
        {
            return UnityEngine.Object.Instantiate(_projectileToShoot, _projectileShooter.position, Quaternion.identity);
        }
        /// <summary>
        /// Determines if the attacker is able to attack.
        /// </summary>
        /// <param name="timeSinceLastAttack">Amount of time since last attack.</param>
        /// <returns>Boolean</returns>
        public bool CanAttack(float timeSinceLastAttack)
        {
            if (timeSinceLastAttack < _shooterAttackTime)
            {
                return false;
            }
            return true;
        }
    }
}

