using System.Collections.Generic;
using System.Linq;
using Saving;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _saveTilePrefab;
    private List<GameObject> _saveTiles;
    [SerializeField] private string StartScene { get; set; } = "Home";
    [SerializeField] private GameObject _saveListContainer;


    /// <summary>
    /// Method called when 'back' button is pressed to send user back to initial start screen
    /// </summary>
    public void BackButton()
    {
        _startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the SaveMenu is activated. Loads saves and creates the save tiles
    /// </summary>
    public void OnEnable()
    {
        List<string> saveFileIds = GameSaveManager.GetGameSaveManager().GetSaveFiles();
        _saveTiles = CreateSaveTiles(saveFileIds);
    }


    /// <summary>
    /// Called when the SaveMenu is deactivated (ie: when going back to initial start menu) to destory unneeded objects
    /// </summary>
    public void OnDisable()
    {
        DestroySaveTiles();
    }

    /// <summary>
    /// Delete instantiated save tiles
    /// </summary>
    private void DestroySaveTiles()
    {
        foreach (GameObject saveTile in _saveTiles)
        {
            Destroy(saveTile);
        }
    }

    /// <summary>
    /// activates saveTile objects so they are visible 
    /// </summary>
    private void ShowSaveTiles(List<GameObject> saveTiles)
    {
        foreach (GameObject saveTile in saveTiles)
        {
            saveTile.SetActive(true);
        }
    }

    /// <summary>
    /// deactivates saveTile objects so they are not visible
    /// </summary>
    private void HideSaveTiles(List<GameObject> saveTiles)
    {
        foreach (GameObject saveTile in saveTiles)
        {
            saveTile.SetActive(false);
        }
    }

    private List<GameObject> CreateSaveTiles(List<string> saveFileIds)
    {
        float offset = (_saveListContainer.GetComponent<RectTransform>().rect.height / 2) - (_saveTilePrefab.GetComponent<RectTransform>().rect.height / 2);
        List<GameObject> saveTiles = new();
        GameObject tile;
        for (int idx = 0; idx < saveFileIds.Count; idx++)
        {
            tile = Instantiate(_saveTilePrefab, _saveListContainer.transform);
            tile.GetComponent<RectTransform>().localPosition += new Vector3(0, -(85 * idx) + offset, 0);
            tile.GetComponentInChildren<TextMeshProUGUI>().text = saveFileIds[idx];
            int _idx = idx;
            tile.GetComponent<Button>().onClick.AddListener(() => SelectSave(saveFileIds[_idx]));
            saveTiles.Append(tile);

        }
        return saveTiles;
    }

    public void SelectSave(string saveId)
    {
        GameSaveManager.GetGameSaveManager().LoadGame(int.Parse(saveId));
        SceneManager.LoadScene(StartScene);
    }
}
