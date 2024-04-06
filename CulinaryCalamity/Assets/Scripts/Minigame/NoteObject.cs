using UnityEngine;

/// <summary>
/// Represents a note object in the mini-game.
/// </summary>
public class NoteObject : MonoBehaviour
{
    private bool _canBePressed;
    private bool _obtained = false;
    private Activator _activator;
    [SerializeField] private KeyCode _keyToPress;
    [SerializeField] private GameObject _note;

    /// <summary>
    /// Finds the Activator GameObject and stores its reference.
    /// </summary>
    void Start()
    {
        _activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
    }

    /// <summary>
    /// Updates the note's behavior based on game mode.
    /// </summary>
    void Update()
    {
        if (_note == null)
        {
            if (gameObject.name.Contains("orange"))
            {
                _keyToPress = KeyCode.Q;
            }
            else if (gameObject.name.Contains("pink"))
            {
                _keyToPress = KeyCode.W;
            }
            else if (gameObject.name.Contains("green"))
            {
                _keyToPress = KeyCode.E;
            }
            else if (gameObject.name.Contains("blue"))
            {
                _keyToPress = KeyCode.R;
            }
        }

        if (MiniGameManager.instance.createMode)
        {
            if (Input.GetKeyDown(_keyToPress))
            {
                Instantiate(_note, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Input.GetKeyDown(_keyToPress))
            {
                if (_canBePressed)
                {
                    MiniGameManager.instance.NoteHit();
                    _obtained = true;
                    _activator.ChangeColorWithDelay(Color.yellow, 0.1f);
                    gameObject.SetActive(false);
                }
            }
        }

        DestroyIfOutOfView();
    }

    /// <summary>
    /// Destroys the game object if it is out of the camera view.
    /// </summary>
    private void DestroyIfOutOfView()
    {
        float rightEdge = transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x;
        float rightEdgeViewport = Camera.main.WorldToViewportPoint
            (new Vector3(rightEdge, transform.position.y, transform.position.z)).x;

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
                if (MiniGameManager.instance.gameObject.activeSelf) // Check if MiniGameManager GameObject is active
                {
                    StartCoroutine(MiniGameManager.instance.ShakeScene());
                }
            }
        }
    }

}
