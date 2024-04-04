using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportManager : MonoBehaviour
{
    public static TransportManager Instance { get; private set; }

    private bool _isTeleporting = false;
    private Transform _currentExit; // To keep track of the exit location

    [SerializeField] private float _exitRadius = 1f; // Minimum distance from the exit to re-enable teleporting
    [SerializeField] private float _teleportDelay = 2f; // Time to wait before actual teleport

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: only if you want it to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RequestTeleport(Transform player, Transform destination)
    {
        if (!_isTeleporting)
        {
            StartCoroutine(TeleportPlayer(player, destination));
        }
    }

    private IEnumerator TeleportPlayer(Transform player, Transform destination)
    {
        _isTeleporting = true;
        Debug.Log("Current location: " + player.position + " Destination: " + destination.position);

        yield return new WaitForSeconds(_teleportDelay); // Wait for the delay before teleporting

        player.position = destination.position;
        _currentExit = destination;

        // Now wait until the player has left the exit radius
        while (Vector3.Distance(player.position, _currentExit.position) < _exitRadius)
        {
            yield return null; // Wait until next frame and check again
        }

        _isTeleporting = false;
        _currentExit = null; // Clear the exit point
    }
}