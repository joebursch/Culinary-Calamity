using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Method called when "Start" button is clicked.
    public void StartGame()
    {
        // Load next scene in the build order.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method called when "Exit" button is clicked.
    public void ExitGame()
    {
        // Terminates the application, effectively ending the game.
        Application.Quit();
    }
}


