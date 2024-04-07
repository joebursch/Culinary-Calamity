using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a teleportation trigger that can transport the player to a designated destination.
/// </summary>
public class Transporter : MonoBehaviour
{
    [SerializeField] private Transform _destination; // Set this in the Inspector

    /// <summary>
    /// Triggers the teleportation process when the player enters the collider.
    /// </summary>
    /// <param name="collision">The collider of the object that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player and not another trigger collider
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            TransportManager.Instance.RequestTeleport(collision.transform, _destination);
        }
    }

}
