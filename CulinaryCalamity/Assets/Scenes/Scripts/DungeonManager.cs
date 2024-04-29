using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private List<RoomManager> rooms;
    [SerializeField] private int itemIdToDrop;
    [SerializeField] private GameObject itemDropLocation;
    private int clearedRoomCount = 0;

    private void Start()
    {
        foreach (RoomManager room in rooms)
        {
            room.OnRoomCleared += HandleRoomCleared; // Subscribe to the OnRoomCleared event
        }
    }

    private void HandleRoomCleared()
    {
        clearedRoomCount++; // Increment the count of cleared rooms

        if (clearedRoomCount == rooms.Count) // Check if all rooms are cleared
        {
            Debug.Log("Dungeon completed!");
            DungeonCompleted();
        }
    }

    private void DungeonCompleted()
    {
        Debug.Log("All rooms in the dungeon have been cleared. Congratulations!");
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (itemDropLocation != null)
        {
            Vector2 dropPosition = itemDropLocation.transform.position;
            ItemManager.GetItemManager().SpawnItem((ItemId)itemIdToDrop, dropPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Item drop location GameObject is not set in DungeonManager.");
        }
    }
}

