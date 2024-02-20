using System.Collections.Generic;
using UnityEngine;


public class Savefile : MonoBehaviour
{
    private int playerHealth; // The player's health
    private List<Item> inventory; // The player's inventory
    private UnityEngine.SceneManagement.Scene currentScene; // Current scene player is in
    private List<Character> allCharacterStats; // Stats of every character
    private List<Quest> questStats; // Quest stats
    private List<Minigame> minigameStats; // <int minigameID, [*minigame stats*]>
    private List<int, object[]> forageables; // <int objectUID, [*object stats*]>
    private List<Item> items; // Items not in any inventory
}