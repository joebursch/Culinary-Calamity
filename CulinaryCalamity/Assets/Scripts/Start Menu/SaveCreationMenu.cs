using UnityEngine.SceneManagement;
using UnityEngine;
using Saving;
using System.Linq;

public class SaveCreationMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _invalidNameWarning;
    [SerializeField] private string StartScene { get; set; } = "Home";
    private string _playerName;


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
        if (!CheckPlayerName())
        {
            DisplayNameInvalidMsg();
            return;
        }

        GameSaveState newSaveState = new(_playerName);
        GameSaveManager.GetGameSaveManager().SetSaveState(newSaveState);
        GameSaveManager.GetGameSaveManager().SaveGame();
        SceneManager.LoadScene(StartScene);
    }

    /// <summary>
    /// Links to input field and updates name when the input is changed
    /// </summary>
    /// <param name="playerName"></param>
    public void NameInputFieldChanged(string playerName)
    {
        _playerName = playerName;
        if (_invalidNameWarning.activeSelf)
        {
            _invalidNameWarning.SetActive(false);
        }
    }

    /// <summary>
    /// Show warning label if name isn't formatted correctly
    /// </summary>
    private void DisplayNameInvalidMsg()
    {
        _invalidNameWarning.SetActive(true);
    }

    /// <summary>
    /// Name hsould only have alphabetic characters
    /// </summary>
    /// <returns></returns>
    private bool CheckPlayerName()
    {
        return _playerName.All(char.IsLetter);
    }
}
