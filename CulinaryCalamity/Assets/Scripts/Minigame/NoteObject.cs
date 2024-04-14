using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a note object in the mini-game.
/// </summary>
public class NoteObject : MonoBehaviour
{
    private bool _canBePressed;
    private bool _obtained = false;
    private Activator _activator;
    [SerializeField] private GameObject _note;


    // input
    private Actions _controlScheme = null;

    /// <summary>
    /// Finds the Activator GameObject and stores its reference.
    /// </summary>
    void Start()
    {
        _activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
        _controlScheme = new Actions();
        _controlScheme.Enable();
    }

    /// <summary>
    /// Updates the note's behavior based on game mode.
    /// </summary>
    void Update()
    {
        if (MiniGameManager.instance.createMode)
        {
            if (_controlScheme.MiniGame.orangeNote.triggered || _controlScheme.MiniGame.pinkNote.triggered ||
                _controlScheme.MiniGame.greenNote.triggered || _controlScheme.MiniGame.blueNote.triggered)
            {
                Instantiate(_note, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (_controlScheme.MiniGame.orangeNote.triggered || _controlScheme.MiniGame.pinkNote.triggered || 
                _controlScheme.MiniGame.greenNote.triggered || _controlScheme.MiniGame.blueNote.triggered)
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
                if (MiniGameManager.instance.gameObject.activeSelf)
                {
                    StartCoroutine(MiniGameManager.instance.ShakeScene());
                }
            }
        }
    }
}
