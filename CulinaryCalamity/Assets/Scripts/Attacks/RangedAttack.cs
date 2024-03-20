using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class RangedAttack : AttackStrategy
    {
        private GameObject _projectileToShoot;
        private Transform _projectileShooter;

        private float _shooterAttackSpeed;

        public RangedAttack(GameObject projectile, Transform shooter, float attackSpeed)
        {
            _projectileToShoot = projectile;
            _projectileShooter = shooter;
            _shooterAttackSpeed = attackSpeed;
        }

        public void Attack()
        {
            //Debug.Log("Ranged Attack Triggered!");
            ShootProjectile();
        }

        // IDK if I should do this
        private void ShootProjectile()
        {
            //Debug.Log("Shooting a projectile!");
            //_projectileToShoot.GetComponent<Projectile>().SetTargetPosition(_projectileShooter._huntingTarget.transform.position);
            UnityEngine.Object.Instantiate(_projectileToShoot, _projectileShooter.position, Quaternion.identity);
        }

        public bool CanAttack(float currentTime)
        {
            if (currentTime < _shooterAttackSpeed)
            {
                return false;
            }
            return true;
        }
    }
}

