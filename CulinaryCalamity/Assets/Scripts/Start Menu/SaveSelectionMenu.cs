using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;

    public void BackButton()
    {
        _startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        // Get saves from GSM
        // Create save panels
        // display save panels
        // do all the necessary linkings
    }
}
