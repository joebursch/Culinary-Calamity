using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public interface AttackStrategy
    {
        /// <summary>
        /// Method for triggering an attack.
        /// </summary>
        /// <param name="targetPosition">Position of the attack victim</param>
        public void Attack(Vector3 targetPosition);
        /// <summary>
        /// Determines if the attacker is able to attack.
        /// </summary>
        /// <param name="timeSinceLastAttack">Amount of time since last attack.</param>
        /// <returns>Boolean</returns>
        public bool CanAttack(float timeSinceLastAttack);
    }
}

