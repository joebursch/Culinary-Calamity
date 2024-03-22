using UnityEditor.ShaderGraph;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Private instance variables
    [SerializeField] protected string characterName;
    protected int movementSpeed;
    [SerializeField] protected float characterHealth;
    protected Animator characterAnimator;
    protected float currentHealth;

    protected Vector2 _movementDir;

    [SerializeField] protected LayerMask _solidObjectsLayer;
    [SerializeField] protected LayerMask _interactableObjectsLayer;
    [SerializeField] protected LayerMask _defaultLayer;
    [SerializeField] protected LayerMask _playerLayer;

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
    /// Configures the animation state for all characters with an animator.
    /// </summary>
    /// <param name="movementDir">Direction of character movement</param>
    protected void ConfigureAnimator(Vector2 movementDir)
    {
        bool moving = movementDir != Vector2.zero;
        if (moving)
        {
            characterAnimator.SetFloat("moveX", movementDir.x);
            characterAnimator.SetFloat("moveY", movementDir.y);
        }
        characterAnimator.SetBool("isWalking", moving);
    }
    /// <summary>
    /// Checks to see if the characters desired movement location is walkable
    /// </summary>
    /// <param name="movementDir"></param>
    /// <returns></returns>
    protected bool IsWalkable(Vector2 movementDir)
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPos = currentPosition + movementSpeed * Time.deltaTime * movementDir;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.2f, _solidObjectsLayer | _interactableObjectsLayer | _defaultLayer | _playerLayer);

        foreach (Collider2D collider in colliders)
        {
            Debug.DrawLine(transform.position, collider.gameObject.transform.position, Color.cyan);
            if (collider.gameObject != gameObject && !collider.isTrigger)
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// Adjust the current health of a character by a specified value.
    /// </summary>
    /// <param name="adjustmentValue">Amount by which to adjust character health.</param>
    protected void SetCurrentHealth(float adjustmentValue)
    {
        currentHealth += adjustmentValue;
    }
    /// <summary>
    /// Receive the current health of a character.
    /// </summary>
    /// <returns>float representation of health</returns>
    protected float GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Method for dealing damage to a Character
    /// </summary>
    /// <param name="damageDealt">Amount of damage to be dealt</param>
    public virtual void TakeDamage(float damageDealt)
    {
        SetCurrentHealth(-damageDealt);
        KnockbackEffect();
        if (currentHealth <= 0) { Death(); }
    }
    /// <summary>
    /// When a character is hit, knock them backwards
    /// </summary>
    protected virtual void KnockbackEffect()
    {
        transform.position = new Vector3(transform.position.x - (_movementDir.x * 2), transform.position.y - (_movementDir.y * 2), transform.position.z);
    }

    /// <summary>
    /// Method for a character's death.
    /// </summary>
    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
