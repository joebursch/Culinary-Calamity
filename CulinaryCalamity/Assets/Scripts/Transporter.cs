using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private Transform _destination; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            TransportManager.Instance.RequestTeleport(collision.transform, _destination);
        }
    }

}
