using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Enemies
{
    public class Dragon : Creature
    {
        void Awake()
        {
            AttackStrategy attackStrategy = new MeleeAttack(_damage, LayerMask.GetMask("Player"), _timeBetweenAttacks);
            InitializeCreature(attackStrategy);
        }

        void Update() => ManageCreatureState();
    }
}

