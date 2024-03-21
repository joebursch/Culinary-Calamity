using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField] private GameObject _enemyToSpawn;
        [SerializeField] private int _minNumToSpawn;
        [SerializeField] private int _maxNumToSpawn;
        private Vector3 _randomSpawnPosition;

        void Awake() => SpawnEnemies();


        /// <summary>
        /// Spawns a random number of enemies.
        /// </summary>
        private void SpawnEnemies()
        {
            var numToSpawn = Random.Range(_minNumToSpawn, _maxNumToSpawn + 1);
            for (int i = 0; i < numToSpawn; i++)
            {
                SetRandomSpawnPosition();
                Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);
            }
        }
        /// <summary>
        /// Sets a random spawn position for the enemies.
        /// </summary>
        private void SetRandomSpawnPosition()
        {
            _randomSpawnPosition.x = Random.Range(-2.0f, 2.0f);
            _randomSpawnPosition.y = Random.Range(-2.0f, 2.0f);
        }
    }
}

