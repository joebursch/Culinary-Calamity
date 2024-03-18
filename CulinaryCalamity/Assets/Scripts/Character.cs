using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables
    [SerializeField] protected string characterName;
    protected int movementSpeed;
    [SerializeField] protected int characterHealth;
    protected Animator characterAnimator;
    protected int currentHealth;

    [SerializeField] private LayerMask _solidObjectsLayer;
    [SerializeField] protected LayerMask _interactableObjectsLayer;

    protected void ConfigureAnimator(Vector2 movementDir, bool running)
    {
        bool moving = movementDir != Vector2.zero;
        if (moving)
        {
            characterAnimator.SetFloat("moveX", movementDir.x);
            characterAnimator.SetFloat("moveY", movementDir.y);
        }
        characterAnimator.SetBool("isRunning", running && moving);
        characterAnimator.SetBool("isWalking", moving);
    }

    protected bool IsWalkable(Vector2 movementDir)
    {
        var targetPos = transform.position;
        targetPos.x += movementDir.x;
        targetPos.y += movementDir.y;
        if (Physics2D.OverlapCircle(targetPos, 0.2f, _solidObjectsLayer | _interactableObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

}



