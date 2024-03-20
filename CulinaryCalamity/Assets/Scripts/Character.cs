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

    /// <summary>
    /// Configures the animation state for all characters with an animator.
    /// </summary>
    /// <param name="movementDir">Direction of character movement</param>
    /// <param name="running">Boolean for if the character is running</param>
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

    /// <summary>
    /// Checks to see if the characters desired movement location is walkable
    /// </summary>
    /// <param name="movementDir"></param>
    /// <returns></returns>
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



