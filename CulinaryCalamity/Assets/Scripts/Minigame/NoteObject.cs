using UnityEngine;

/// <summary>
/// Represents a note object in the mini-game.
/// </summary>
public class NoteObject : MonoBehaviour
{
    private bool _canBePressed;
    private bool _obtained = false;
    private Activator _activator;
    [SerializeField] private GameObject _note;

    private Actions _controlScheme = null;
    private SpriteRenderer _spriteRenderer;


    /// <summary>
    /// Finds the Activator GameObject and stores its reference. Also sets up input actions.
    /// </summary>
    void Start()
    {
        _activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
        _controlScheme = new Actions();
        _controlScheme.Enable();
        _spriteRenderer = GetComponent<SpriteRenderer>();


    }

    /// <summary>
    /// Updates the note's behavior based on the game mode.
    /// </summary>
    void Update()
    {
        if (MiniGameManager.instance.createMode)
        {
            if (CorrectNoteInputTriggered())
            {
                InstantiateNote();
            }
        }
        else
        {
            if (_canBePressed && CorrectNoteInputTriggered())
            {
                HandleNoteHit();
                Debug.Log("NOTE HIT");
            }
            else if (!_canBePressed && AnyNoteInputTriggered())
            {
                if (FindClosestNote() == this)
                {
                    NoteHitEarly();
                    Debug.Log("NOTE EARLY");
                }
            }
        }

        DestroyIfOutOfView();
    }



    /// <summary>
    /// Checks if the correct note input action is triggered based on the tag of the note object.
    /// </summary>
    /// <returns>True if the correct input action is triggered for the note object; otherwise, false.</returns>
    bool CorrectNoteInputTriggered()
    {

        ///if something can be 

        return (_controlScheme.MiniGame.orangeNote.triggered && CompareTag("Qnote")) ||
               (_controlScheme.MiniGame.pinkNote.triggered && CompareTag("Wnote")) ||
               (_controlScheme.MiniGame.greenNote.triggered && CompareTag("Enote")) ||
               (_controlScheme.MiniGame.blueNote.triggered && CompareTag("Rnote"));
    }

    bool AnyNoteInputTriggered()
    {
        return _controlScheme.MiniGame.orangeNote.triggered ||
               _controlScheme.MiniGame.pinkNote.triggered ||
               _controlScheme.MiniGame.greenNote.triggered ||
               _controlScheme.MiniGame.blueNote.triggered;
    }
    /// <summary>
    /// Instantiates the note object at the current position.
    /// </summary>
    void InstantiateNote()
    {
        Instantiate(_note, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Handles a successful note hit
    /// </summary>
    void HandleNoteHit()
    {
        MiniGameManager.instance.NoteHit();
        _obtained = true;
        _activator.ChangeColorWithDelay(Color.yellow, 0.15f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroys the game object if it is out of the camera view.
    /// </summary>
    private void DestroyIfOutOfView()
    {
        float rightEdge = transform.position.x + _spriteRenderer.bounds.extents.x;
        float rightEdgeViewport = Camera.main.WorldToViewportPoint(new Vector3(rightEdge, transform.position.y, transform.position.z)).x;

        if (!_obtained && rightEdgeViewport < 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Triggered when collider enters activator zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == _activator.gameObject)
        {
            _canBePressed = true;
        }
    }

    /// <summary>
    /// Triggered when collider exits activator zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == _activator.gameObject)
        {
            _canBePressed = false;
            if (!_obtained)
            {
                MiniGameManager.instance.NoteMissed();
                _activator.ChangeColorWithDelay(Color.red, 0.1f);
                StartCoroutine(MiniGameManager.instance.ShakeScene());
            }
        }
    }

    /// <summary>
    /// Handles notes hit early by resetting the multiplier, updating the UI, and deactivating or destroying the note.
    /// </summary>
    public void NoteHitEarly()
    {
        if (_obtained)
        {
            return;
        }

        NoteObject closestNote = FindClosestNote();

        // Destroy the closest missed note if found
        if (closestNote != null && closestNote == this)
        {
            Destroy(closestNote.gameObject);
        }
    }

    private NoteObject FindClosestNote()
    {
        Vector3 activatorPosition = _activator.transform.position;

        float minDistance = float.MaxValue;
        NoteObject closestNote = null;

        foreach (var note in FindObjectsOfType<NoteObject>())
        {
            if (note.transform.position.x <= activatorPosition.x)
                continue;

            if (!note._obtained)
            {
                float distance = note.transform.position.x - activatorPosition.x;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestNote = note;
                }
            }
        }
        return closestNote;
    }
}
