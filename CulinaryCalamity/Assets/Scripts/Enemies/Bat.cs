using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Enemies
{
    public class Bat : Creature
    {
        void Awake()
        {
            AttackStrategy attackStrategy = new RangedAttack();
            InitializeCreature(attackStrategy);
        }

        void Update() => ManageCreatureState();
    }
}

