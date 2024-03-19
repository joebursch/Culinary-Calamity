using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class Bat : Creature
    {
        void Awake() => InitializeCreature();

        void Update() => ManageCreatureState();
    }
}

