using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemies
{
    public class Duck : Creature
    {
        void Awake() => InitializeCreature();

        void Update() => ManageCreatureState();
    }
}
