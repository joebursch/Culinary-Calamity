using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Enemies
{
    public class Duck : Creature
    {
        [SerializeField] private GameObject _duckProjectile;
        private AttackStrategy _attackStrategy;
        void Awake()
        {
            _attackStrategy = new RangedAttack(_duckProjectile, transform, _timeBetweenAttacks);
            InitializeCreature(_attackStrategy);
        }

        void Update()
        {
            ManageCreatureState();
        }
    }
}
