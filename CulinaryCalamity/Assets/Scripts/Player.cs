using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Player : Character
{
   [SerializeField] private List<Item> playerInventory; // Update to Inventory reference
   [SerializeField] private int amountOfGold;
   private Questline questline;
   private int currentHealth; 
   private Actions controlScheme = null;
   private Vector2 movementDir;

   void Awake() => controlScheme = new Actions();

   void OnEnable() => controlScheme.Standard.Enable();

   void OnDestroy() => controlScheme.Standard.Disable();

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
