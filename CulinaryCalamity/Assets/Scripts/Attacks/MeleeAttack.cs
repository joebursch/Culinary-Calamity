using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public class MeleeAttack : AttackStrategy
    {
        public void Attack()
        {
            Debug.Log("Melee Attack triggered!");
        }
    }
}

