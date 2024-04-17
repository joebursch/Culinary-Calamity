using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private Actions _controlScheme;

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
        _controlScheme.Enable();
    }

    private void OnDestroy()
    {
        _controlScheme.Disable();
    }

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

    public bool AnyNoteInputTriggered()
    {
        return _controlScheme.MiniGame.orangeNote.triggered ||
               _controlScheme.MiniGame.pinkNote.triggered ||
               _controlScheme.MiniGame.greenNote.triggered ||
               _controlScheme.MiniGame.blueNote.triggered;
    }
}
