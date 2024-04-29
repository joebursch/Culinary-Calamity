using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Creature> enemies;
    private bool _isCleared = false;
    public event Action OnRoomCleared;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.OnCreatureDeath += HandleEnemyDeath; // Subscribe to the OnDeath event
        }
    }

    private void HandleEnemyDeath(Creature enemy)
    {
        enemies.Remove(enemy); // Remove the enemy from the list

        if (enemies.Count == 0 && !_isCleared)
        {
            _isCleared = true;
            Debug.Log("Room cleared!");
            OnRoomCleared?.Invoke(); // Invoke the room cleared event
        }
    }
}
