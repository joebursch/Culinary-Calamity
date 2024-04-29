using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _projectileDamage;
    [SerializeField] private float _projectileLifetime;
    private Vector2 _movementDirection;

    #region UnityBuiltIn

    void Awake()
    {
        Destroy(gameObject, _projectileLifetime);
    }
    void Update()
    {
        transform.Translate(_movementDirection * _projectileSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
    #endregion


    /// <summary>
    /// Gets the direction for the projectile to move in.
    /// </summary>
    /// <returns>Vector2</returns>
    private Vector2 GetMovementDirection()
    {
        var moveDir = new Vector2((_targetPosition.x - transform.position.x), (_targetPosition.y - transform.position.y));
        moveDir.Normalize();
        return moveDir;
    }
    /// <summary>
    /// Sets the targeting position of the projectile.
    /// </summary>
    /// <param name="targetPosition">position of target character</param>
    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _movementDirection = GetMovementDirection();
    }
    /// <summary>
    /// Get the damage dealt by a projectile
    /// </summary>
    /// <returns>float representation of damage</returns>
    public float GetProjectileDamage()
    {
        return _projectileDamage;
    }

}
