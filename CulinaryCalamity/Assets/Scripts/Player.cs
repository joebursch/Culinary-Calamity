using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Player : Character
{
   private int currentHealth; 
   [SerializeField]
   private List<Item> playerInventory; 
   [SerializeField]
   private int amountOfGold;
   private Animation attackAnimation;
   private Animation cookingAnimation;
   private Actions controlScheme = null;

   private Vector2 movementDir;

   void Awake() => controlScheme = new Actions();

   void OnEnable() => controlScheme.Standard.Enable();

   void OnDestroy() => controlScheme.Standard.Disable();

   // Constructors
   public Player(string name, float speed, int health, List<Item> inventory, int gold) : base(name, speed, health)
   {
        playerInventory = inventory;
        amountOfGold = gold; 
   }

   public Player(string name, Sprite sprite, float speed, int health, List<Animation> animations, List<Item> inventory, int gold, Animation attack, Animation cook) 
    : base(name, sprite, speed, health, animations)
   {
        playerInventory = inventory;
        amountOfGold = gold; 
        attackAnimation = attack;
        cookingAnimation = cook;
   }

   void movePlayer()
   {

     movementDir = controlScheme.Standard.Move.ReadValue<Vector2>();
     if(movementDir.x > 0 | movementDir.x < 0){movementDir.y = 0;}
     transform.Translate(movementDir * movementSpeed * Time.deltaTime);
     
   }

   // Player movement 
   void Update() 
   {
     movePlayer();
   }
}
