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
            Debug.Log(currentHealth);
            AttackStrategy attackStrategy = new RangedAttack(_batProjectile, transform, _timeBetweenAttacks);
            InitializeCreature(attackStrategy);
        }

        void Update() => ManageCreatureState();
    }
}

