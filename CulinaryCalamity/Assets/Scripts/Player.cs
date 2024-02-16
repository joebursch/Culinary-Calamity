using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   // Private instance variables
   private int currentHealth; 
   private List<string> playerInventory; // List of Items in future?

   private int amountOfGold;

   // public instance variables
   public Animation attackAnimation;
   public Animation cookingAnimation;



   // Constructors
   public Player(string name, float speed, int health, List<string> inventory, int gold) : base(name, speed, health)
   {
        playerInventory = inventory;
        amountOfGold = gold; 
   }

   public Player(string name, Sprite sprite, float speed, int health, List<Animation> animations, List<string> inventory, int gold, Animation attack, Animation cook) 
    : base(name, sprite, speed, health, animations)
   {
        playerInventory = inventory;
        amountOfGold = gold; 
        attackAnimation = attack;
        cookingAnimation = cook;
   }
}
