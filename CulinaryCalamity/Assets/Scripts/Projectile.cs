using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _projectileLifetime;

    #region UnityBuiltIn

    void Awake()
    {
        Destroy(gameObject, _projectileLifetime);
    }
    void Update()
    {
        var _movementDirection = GetMovementDirection();
        transform.Translate(_movementDirection * _projectileSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }


    #endregion

    private Vector2 GetMovementDirection()
    {
        var moveDir = new Vector2((_targetPosition.x - transform.position.x), (_targetPosition.y - transform.position.y));
        moveDir.Normalize();
        return moveDir;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        Debug.Log("Set Target Position " + targetPosition);
        _targetPosition = targetPosition;
    }

}
