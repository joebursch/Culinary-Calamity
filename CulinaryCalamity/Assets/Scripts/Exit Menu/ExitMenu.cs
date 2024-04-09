using Unity.Loading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    private static string lastSceneName;
    private static Vector3 playerPos;

    /// <summary>
    /// Method called when the "Yes" button is clicked.
    /// </summary>
    public void YesButton()
    {
        // Terminates the application, effectively ending the game.
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        Application.Quit();
#endif
    }

    /// <summary>
    /// Method called when the "No" button is clicked.
    /// </summary>
    public void NoButton()
    {
        SceneManager.LoadSceneAsync(lastSceneName);
        playerPos = new Vector3(playerPos.x, playerPos.y, playerPos.z + 100);
        GameObject.FindAnyObjectByType<Player>().transform.position = playerPos;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Method to receive player data and put it into private static variables.
    /// </summary>
    /// <param name="sceneName">The string name of the scene before opening the exit menu.</param>
    /// <param name="loweredPlayerPos">The position of the player after they are lowered below the world.</param>
    public void SetPlayerData(string sceneName, Vector3 loweredPlayerPos)
    {
        lastSceneName = sceneName;
        playerPos = loweredPlayerPos;
    }
}
