using UnityEngine;

public class ControlSchemeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;

    public void BackButton()
    {
        _startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
