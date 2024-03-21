using UnityEditor;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _saveSelectMenu;

    /// <summary>
    /// Method called when the "Start" button is clicked.
    /// </summary>
    public void StartButton()
    {
        _saveSelectMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Method called when the "Exit" button is clicked.
    /// </summary>
    public void ExitButton()
    {
        // Terminates the application, effectively ending the game.
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            Application.Quit();
#endif
    }
}


