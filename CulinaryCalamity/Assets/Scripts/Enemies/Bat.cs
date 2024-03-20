using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Enemies
{
    public class Bat : Creature
    {
        [SerializeField] private GameObject _batProjectile;
        void Awake()
        {
            AttackStrategy attackStrategy = new RangedAttack(_batProjectile, transform, _creatureAttackSpeed);
            InitializeCreature(attackStrategy);
        }

        void Update() => ManageCreatureState();
    }
}

