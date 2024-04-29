using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Enemies
{
    public class Duck : Creature
    {
        [SerializeField] private GameObject _duckProjectile;

        void Awake()
        {
            AttackStrategy _attackStrategy = new RangedAttack(_duckProjectile, transform, _timeBetweenAttacks);
            InitializeCreature(_attackStrategy);
        }

        void Update() => ManageCreatureState();

    }
}
