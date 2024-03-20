using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCreationMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private string StartScene { get; set; } = "Home";

    /// <summary>
    /// Called on back button press to return to start menu
    /// </summary>
    public void BackButton()
    {
        _startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called on Start game button press to create save file and load the game intro scene
    /// </summary>
    public void StartGameButton()
    {

    }
}
