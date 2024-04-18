using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private bool _canBePressed;
    public bool _obtained = false;
    private Activator _activator;
    [SerializeField] private GameObject _note;

    private List<NoteObject> _noteList = new List<NoteObject>();
    private int _currentNoteIndex = 0;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Find all NoteObject instances and add them to the list
        NoteObject[] notes = FindObjectsOfType<NoteObject>();
        _noteList.AddRange(notes);

        // Sort the list based on the initial x-coordinate positions of the notes
        _noteList.Sort((note1, note2) => note1.transform.position.x.CompareTo(note2.transform.position.x));
    }

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
                Debug.Log("NOTE HIT");
            }
            else if (!_obtained && !_canBePressed && InputManager.instance.AnyNoteInputTriggered())
            {
                // Check if the current note is hit early
                if (_noteList[_currentNoteIndex] == this)
                {
                    NoteHitEarly();
                    Debug.Log("NOTE EARLY");
                }
            }
        }

        DestroyIfOutOfView();
    }

    void InstantiateNote()
    {
        Instantiate(_note, transform.position, Quaternion.identity);
    }

    void HandleNoteHit()
    {
        MiniGameManager.instance.NoteHit();
        _obtained = true;
        _activator.ChangeColorWithDelay(Color.yellow, 0.15f);
        gameObject.SetActive(false);

        // Move to the next note in the list
        _currentNoteIndex++;
    }

    private void DestroyIfOutOfView()
    {
        float rightEdge = transform.position.x + _spriteRenderer.bounds.extents.x;
        float rightEdgeViewport = Camera.main.WorldToViewportPoint(new Vector3(rightEdge, transform.position.y, transform.position.z)).x;

        if (!_obtained && rightEdgeViewport < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == _activator.gameObject)
        {
            _canBePressed = true;
        }
    }

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

    public void NoteHitEarly()
    {
        if (!_obtained)
        {
            // Destroy the current note
            Destroy(_noteList[_currentNoteIndex].gameObject);

            // Move to the next note in the list
            _currentNoteIndex++;

            if (_currentNoteIndex >= _noteList.Count)
            {
                // If there are no more notes in the list, reset the index
                _currentNoteIndex = 0;
            }
        }
    }





}
