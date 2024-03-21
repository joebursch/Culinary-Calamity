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

        public RangedAttack(GameObject projectile, Transform shooter, float timeBetweenAttacks)
        {
            _projectileToShoot = projectile;
            _projectileShooter = shooter;
            _shooterAttackTime = timeBetweenAttacks;
        }

        public void Attack(Vector3 targetPosition)
        {
            ShootProjectile().GetComponent<Projectile>().SetTargetPosition(targetPosition);
        }

        // IDK if I should do this
        private GameObject ShootProjectile()
        {
            return UnityEngine.Object.Instantiate(_projectileToShoot, _projectileShooter.position, Quaternion.identity);
        }

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

