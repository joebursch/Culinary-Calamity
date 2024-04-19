using UnityEngine;


/// <summary>
/// Manages input actions for the mini-game.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Actions _controlScheme;

    /// <summary>
    /// Initializes the singleton instance and enables input actions.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _controlScheme = new Actions();
        _controlScheme.MiniGame.Enable();
    }

    /// <summary>
    /// Disables input actions when the object is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        _controlScheme.Disable();
    }

    /// <summary>
    /// Checks if the correct note input is triggered based on the provided note tag.
    /// </summary>
    /// <param name="noteTag"></param>
    /// <returns>True if the correct note input is triggered, otherwise false.</returns>
    public bool CorrectNoteInputTriggered(string noteTag)
    {
        switch (noteTag)
        {
            case "Qnote":
                return _controlScheme.MiniGame.orangeNote.triggered;
            case "Wnote":
                return _controlScheme.MiniGame.pinkNote.triggered;
            case "Enote":
                return _controlScheme.MiniGame.greenNote.triggered;
            case "Rnote":
                return _controlScheme.MiniGame.blueNote.triggered;
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks if any note input is triggered.
    /// </summary>
    /// <returns>True if any note input is triggered, otherwise false.</returns>
    public bool AnyNoteInputTriggered()
    {
        return _controlScheme.MiniGame.orangeNote.triggered ||
               _controlScheme.MiniGame.pinkNote.triggered ||
               _controlScheme.MiniGame.greenNote.triggered ||
               _controlScheme.MiniGame.blueNote.triggered;
    }
}
