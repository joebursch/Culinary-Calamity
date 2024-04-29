using UnityEngine;

/// <summary>
/// Represents a note object in the mini-game.
/// </summary>
public class NoteObject : MonoBehaviour
{
    private bool _canBePressed;
    public bool _obtained = false;
    private Activator _activator;
    [SerializeField] private GameObject _note;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Finds the Activator GameObject and stores its reference, 
    /// and gets the sprite renderer component.
    /// </summary>
    void Start()
    {
        _activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Updates the note's behavior based on game mode.
    /// </summary>
    void Update()
    {
        if (MiniGameManager.instance.createMode)
        {
            // Instantiate note if in create mode
            if (InputManager.instance.AnyNoteInputTriggered())
            {
                InstantiateNote();
            }
        }
        else
        {
            if (_canBePressed && InputManager.instance.CorrectNoteInputTriggered(gameObject.tag))
            {
                HandleNoteHit();
            }
        }

        DestroyIfOutOfView();
    }

    /// <summary>
    /// Instantiates a note object.
    /// </summary>
    void InstantiateNote()
    {
        Instantiate(_note, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Handles the note hit event.
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
}
