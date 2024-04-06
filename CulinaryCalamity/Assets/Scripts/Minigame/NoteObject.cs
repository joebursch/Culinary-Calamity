using UnityEngine;

/// <summary>
/// Represents a note object in the mini-game.
/// </summary>
public class NoteObject : MonoBehaviour
{
    private bool canBePressed;
    private bool obtained = false;
    private Activator activator;
    [SerializeField] private KeyCode keyToPress;
    [SerializeField] private GameObject note;

    /// <summary>
    /// Finds the Activator GameObject and stores its reference.
    /// </summary>
    void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
    }

    /// <summary>
    /// Updates the note's behavior based on game mode.
    /// </summary>
    void Update()
    {
        if (MiniGameManager.instance.createMode)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                Instantiate(note, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (canBePressed)
                {
                    MiniGameManager.instance.NoteHit();
                    obtained = true;
                    activator.ChangeColorWithDelay(Color.yellow, 0.1f);
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

        if (!obtained && rightEdgeViewport < 0)
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
        if (!MiniGameManager.instance.createMode && other.gameObject == activator.gameObject)
        {
            canBePressed = true;
        }
    }

    /// <summary>
    /// Triggered when collider exits activator zone.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == activator.gameObject)
        {
            canBePressed = false;
            if (!obtained)
            {
                MiniGameManager.instance.NoteMissed();
                activator.ChangeColorWithDelay(Color.red, 0.1f);
                if (MiniGameManager.instance.gameObject.activeSelf) // Check if MiniGameManager GameObject is active
                {
                    StartCoroutine(MiniGameManager.instance.ShakeScene());
                }
            }
        }
    }

}
