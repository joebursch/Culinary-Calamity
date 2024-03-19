using System;
using System.Collections;
using System.Collections.Generic;
using Saving;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _saveTilePrefab;
    private List<GameObject> _saveTiles;
    public string startScene = "Home";

    public void BackButton()
    {
        _startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        List<string> saveFileIds = GameSaveManager.GetGameSaveManager().GetSaveFiles();
        _saveTiles = CreateSaveTiles(saveFileIds);
    }

    private void DisplaySaveTiles(List<GameObject> saveTiles)
    {

    }

    private List<GameObject> CreateSaveTiles(List<string> saveFileIds)
    {

        foreach (string saveFile in saveFileIds)
        {
            GameObject tile = Instantiate(_saveTilePrefab);
        }
        return null;
    }

    private List<Tuple<int, Vector3>> GetSaveTileSpawnPositions(int numPositions)
    {
        return null;
    }
    public void SelectSave(string saveId)
    {
        GameSaveManager.GetGameSaveManager().LoadGame(int.Parse(saveId));
        SceneManager.LoadScene(startScene);
    }
}
