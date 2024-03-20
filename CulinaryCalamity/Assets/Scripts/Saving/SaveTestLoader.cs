using Saving;
using UnityEngine;

/// <summary>
/// Used for testing Saving
/// </summary>
public class SaveTestLoader : MonoBehaviour
{
    int saveid;
    // Start is called before the first frame update
    void Start()
    {
        GameSaveState save = new();
        saveid = save.SaveId;
        GameSaveManager.GetGameSaveManager().SetSaveState(save);
    }

    public void SaveClick()
    {
        GameSaveManager.GetGameSaveManager().SaveGame();
    }

    public void LoadClick()
    {
        GameSaveManager.GetGameSaveManager().LoadGame(saveid);
    }
}
