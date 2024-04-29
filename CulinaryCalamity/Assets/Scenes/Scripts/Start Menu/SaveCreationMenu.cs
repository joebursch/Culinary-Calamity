using UnityEngine.SceneManagement;
using UnityEngine;
using Saving;
using System.Linq;

public class SaveCreationMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _invalidNameWarning;
    [SerializeField] private string StartScene { get; set; } = "Home";
    private string _playerName = "";


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

        GameSaveState newSaveState = new();
        GameSaveManager gsm = GameSaveManager.GetGameSaveManager();

        gsm.SetSaveState(newSaveState);
        gsm.UpdateObjectSaveData("PlayerObject", Player.CreateInitialPlayerSaveData(_playerName));
        GameSaveManager.GetGameSaveManager().SaveGame();
        Saver.UpdateSaveIndex(newSaveState.SaveId, _playerName);
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
        return _playerName.Length > 0 && _playerName.All(char.IsLetter);
    }
}
