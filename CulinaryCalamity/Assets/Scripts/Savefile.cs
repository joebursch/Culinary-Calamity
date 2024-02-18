using System.Collections.Generic;
using UnityEngine;


public class Savefile : MonoBehaviour
{
    private int playerHealth; // The player's health
    private Item[] inventory; // The player's inventory
    private UnityEngine.SceneManagement.Scene currentScene; // Current scene player is in
    private Dictionary<int, object[]> characterStats; // <int characterUID, [int characterID, int characterHealth, int characterTimesKilled, Vector3 characterLocation, Item[] characterInventory]>
    private Dictionary<int, object[]> quests; // <int questID, [*quest stats*]>
    private Dictionary<int, object[]> minigame; // <int minigameID, [*minigame stats*]>
    private Dictionary<int, object[]> forageables; // <int objectUID, [*object stats*]>
}