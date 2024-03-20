using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
    public interface AttackStrategy
    {
        public void Attack(Vector3 targetPosition);
        public bool CanAttack(float currentTime);
    }
}

