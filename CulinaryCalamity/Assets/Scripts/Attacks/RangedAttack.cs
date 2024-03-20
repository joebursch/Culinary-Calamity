using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class RangedAttack : AttackStrategy
    {

        public void Attack()
        {
            Debug.Log("Ranged Attack Triggered!");
        }

        // IDK if I should do this
        private void ShootProjectile(GameObject projectile, Vector3 spawnPosition, Vector3 targetPosition)
        {
            Debug.Log("Shooting a projectile!");
        }
    }
}

