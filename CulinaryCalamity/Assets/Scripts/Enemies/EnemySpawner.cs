using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private int _minNumToSpawn;
    [SerializeField] private int _maxNumToSpawn;

    private Vector3 _randomSpawnPosition;

    void Awake()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var numToSpawn = Random.Range(_minNumToSpawn, _maxNumToSpawn + 1);
        for (int i = 0; i < numToSpawn; i++)
        {
            SetRandomSpawnPosition();
            Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);
        }
    }

    private void SetRandomSpawnPosition()
    {
        _randomSpawnPosition.x = Random.Range(-2.0f, 2.0f);
        _randomSpawnPosition.y = Random.Range(-2.0f, 2.0f);
    }
}
