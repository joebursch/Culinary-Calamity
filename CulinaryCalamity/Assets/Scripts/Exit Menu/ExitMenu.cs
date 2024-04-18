using Unity.Loading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    private static string lastSceneName;

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
        Toggle();
    }

    /// <summary>
    /// display/hide the exit menu
    /// </summary>
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
