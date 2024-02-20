using System.Collections.Generic;
using UnityEngine;


public class Savefile : MonoBehaviour
{
    private Player player; // To save player's health, inventory, and other information
    private List<Character> allCharacterStats; // Get the information from every character in the game to trace position/status
    private List<Quest> questStats;
    private List<Minigame> minigameStats;
    private List<Item> items; // Items not in any inventory (on ground, etc.)
}