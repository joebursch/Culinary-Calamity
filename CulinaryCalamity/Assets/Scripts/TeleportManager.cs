using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the teleportation of a player between different locations in the game.
/// </summary>
public class TransportManager : MonoBehaviour
{
    public static TransportManager Instance { get; private set; }

    private bool _isTeleporting = false;
    private Transform _currentExit; // To keep track of the exit location

    [SerializeField] private FadeEffect fadeEffect;
    [SerializeField] private float _exitRadius = 1f; // Minimum distance from the exit to re-enable teleporting
    [SerializeField] private float _teleportDelay = 2f; // Time to wait before actual teleport
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    /// <summary>
    /// Initializes the singleton instance of the TransportManager.
    /// </summary>
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

    /// <summary>
    /// Requests a teleport for the player to a specified destination.
    /// </summary>
    /// <param name="player">The transform of the player to teleport.</param>
    /// <param name="destination">The destination transform to teleport the player to.</param>
    public void RequestTeleport(Transform player, Transform destination)
    {
        if (!_isTeleporting)
        {
            StartCoroutine(TeleportPlayer(player, destination));
        }
    }

    /// <summary>
    /// Coroutine that handles the teleportation process, including fading and waiting for the player to move away from the exit point.
    /// </summary>
    /// <param name="player">The transform of the player to teleport.</param>
    /// <param name="destination">The destination transform to teleport the player to.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator TeleportPlayer(Transform player, Transform destination)
    {
        Player playerComponent = player.GetComponent<Player>();

        _isTeleporting = true;
        playerComponent.StartTeleportation();

        fadeEffect.FadeToBlack(fadeDuration);
        yield return new WaitForSeconds(_teleportDelay);

        player.position = destination.position;
        _currentExit = destination;

        fadeEffect.FadeFromBlack(fadeDuration);
        yield return new WaitForSeconds(_teleportDelay);
        playerComponent.EndTeleportation();
        _isTeleporting = false;

        Debug.Log("TeleportManager waiting for player to leave exit radius");
        // Now wait until the player has left the exit radius
        while (Vector3.Distance(player.position, _currentExit.position) < _exitRadius)
        {
            yield return null; // Wait until next frame and check again
        }

        _currentExit = null; // Clear the exit point
    }


}