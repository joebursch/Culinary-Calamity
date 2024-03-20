using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class MeleeAttack : AttackStrategy
    {
        public void Attack(Vector3 targetPosition)
        {
            Debug.Log("Melee Attack triggered!");
        }

        public bool CanAttack(float currentTime)
        {
            // Implement Later
            return false;
        }
    }
}

