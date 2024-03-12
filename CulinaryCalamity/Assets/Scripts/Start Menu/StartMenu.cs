using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    /// <summary>
    /// Method called when the "Start" button is clicked.
    /// </summary>
    public void StartGame()
    {
        // Load next scene in the build order.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Method called when the "Exit" button is clicked.
    /// </summary>
    public void ExitGame()
    {
        // Terminates the application, effectively ending the game.
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            Application.Quit();
#endif
    }
}


