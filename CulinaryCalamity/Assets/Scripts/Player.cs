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
   private Actions controlScheme = null;
   private Vector2 movementDir;
   [SerializeField] private LayerMask solidObjectsLayer;

   void Awake() => controlScheme = new Actions();

   void OnEnable() => controlScheme.Standard.Enable();

   void OnDestroy() => controlScheme.Standard.Disable();

   void movePlayer()
   {
     // Get vector values for movement
     movementDir = controlScheme.Standard.Move.ReadValue<Vector2>();
     // Limit movement to up/down/left/right (No diagonal movement)
     if(movementDir.x > 0 | movementDir.x < 0){movementDir.y = 0;}
     // Check if the target position is walkable
     var targetPos = transform.position;
     targetPos.x += movementDir.x;
     targetPos.y += movementDir.y;
     if(IsWalkable(targetPos))
     {
      transform.Translate(movementDir * movementSpeed * Time.deltaTime);
     }
   }

   private bool IsWalkable(Vector3 targetPos)
   {
      if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
      {
        return false;
      }

      return true;
   }

   // Player movement 
   void Update() 
   {
     movePlayer();
   }
}
