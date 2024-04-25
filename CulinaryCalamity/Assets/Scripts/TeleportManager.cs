using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the teleportation of a player between different locations in the game.
/// </summary>
public class TransportManager : MonoBehaviour
{
    public static TransportManager Instance { get; private set; }

    private bool _isTeleporting = false;
    private Transform _currentExit; // To keep track of the exit location

    [SerializeField] private FadeEffect _fadeEffect;
    [SerializeField] private float _exitRadius = 1f; // Minimum distance from the exit to re-enable teleporting
    [SerializeField] private float _fadeDuration = 1f;

    /// <summary>
    /// Initializes the singleton instance of the TransportManager.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static TransportManager GetTransportManager()
    {
        return Instance;
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

        yield return _fadeEffect.FadeToBlackCoroutine(_fadeDuration);

        player.position = destination.position;
        _currentExit = destination;

        yield return _fadeEffect.FadeFromBlackCoroutine(_fadeDuration);
        playerComponent.EndTeleportation();
        _isTeleporting = false;

        // Now wait until the player has left the exit radius
        while (Vector3.Distance(player.position, _currentExit.position) < _exitRadius)
        {
            yield return null; // Wait until next frame and check again
        }

        _currentExit = null; // Clear the exit point
    }
    // public IEnumerator TeleportPlayerOutOfMiniGame(Transform player, string sceneName)
    // {
    //     Player playerComponent = player.GetComponent<Player>();

    //     playerComponent.StartTeleportation();

    //     SceneManager.LoadScene(sceneName);

    //     playerComponent.EndTeleportation();

    //     yield return _fadeEffect.FadeFromBlackCoroutine(_fadeDuration);

    // }
    public IEnumerator TeleportPlayerAcrossScenes(Transform player, Door activeDoor)
    {
        Player playerComponent = player.GetComponent<Player>();

        playerComponent.StartTeleportation();

        yield return _fadeEffect.FadeToBlackCoroutine(_fadeDuration);

        SceneManager.LoadScene(activeDoor.GetDestinationSceneName());
        playerComponent.EndTeleportation(activeDoor);

        yield return _fadeEffect.FadeFromBlackCoroutine(_fadeDuration);
    }
}