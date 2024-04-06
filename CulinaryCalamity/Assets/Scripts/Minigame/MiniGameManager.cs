using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the gameplay mechanics of the mini-game, including score calculation,
/// multiplier tracking, note streak management, and game state updates. 
/// Also responsible screen shaking effect to enhance player feedback.
public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool _startPlaying;
    [SerializeField] private BeatScroller _theBS;
    [SerializeField] private int _scorePerNote = 100;
    [SerializeField] private int[] _multiplierThresholds;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _multiText;
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private GameObject _resultPanel;

    public static MiniGameManager instance;
    public bool createMode;

    private GameObject _playerObject;
    private ResultPanel _Resultpanel;
    private Transform _cameraTransform;
    private Vector3 _originalCameraPosition;
    private float _shakeDuration = 0.2f;
    private float _shakeMagnitude = 0.1f;
    private int _currentScore;
    private int _currentMultiplier;
    private int _multiplierTracker;
    private int _notesHit;
    private int _notesMissed;
    private int _noteStreak;
    private int _goldEarned;
    private int _currentNoteStreak;
    private float _percentHit;


    /// <summary>
    /// Initializes the MiniGameManager instance and sets up necessary components.
    /// </summary>
    void Start()
    {
        instance = this;
        _playerObject = FindObjectOfType<Player>()?.gameObject;
        DisablePlayerObject();
        _startText.gameObject.SetActive(true);
        _scoreText.text = "Score: 0 ";
        _multiText.text = "Multiplier x1 ";
        _noteStreak = 0;
        _currentNoteStreak = 0;
        _currentMultiplier = 1;
        _Resultpanel = _resultPanel.GetComponent<ResultPanel>();
        _resultPanel.SetActive(false);
        _cameraTransform = Camera.main.transform;
        _originalCameraPosition = _cameraTransform.position;
    }

    /// <summary>
    /// Updates the game state every frame.
    /// </summary>
    void Update()
    {
        if (!_startPlaying)
        {
            if (Input.anyKeyDown)
            {
                StartGame();
            }

        }
        else
        {
            if (IsGameOver())
            {
                GameOver();
            }
        }
    }

    /// <summary>
    /// Handles successful note hits by incrementing the score, updating the multiplier, 
    /// and calculating gold earned. Also maintains the note streak.
    /// </summary>
    public void NoteHit()
    {
        if (!createMode)
        {
            _notesHit++;
            _currentNoteStreak++;
            UpdateNoteStreak();
            UpdateMultiplier();
            UpdateScore();
        }
    }

    /// <summary>
    /// Handles missed notes by resetting the multiplier and updating the UI.
    /// </summary>
    public void NoteMissed()
    {
        if (!createMode)
        {
            _notesMissed++;
            _currentNoteStreak = 0;
            _currentMultiplier = 1;
            _multiplierTracker = 0;
            UpdateMultiplier();
        }
    }

    /// <summary>
    /// Coroutine for shaking the camera to add visual effect.
    /// </summary>
    /// <returns>An IEnumerator for Unity coroutine.</returns>
    public IEnumerator ShakeScene()
    {
        if (_cameraTransform == null)
        {
            yield break;
        }

        float elapsed = 0.0f;

        while (elapsed < _shakeDuration)
        {
            if (_cameraTransform == null)
            {
                yield break;
            }
            float x = _originalCameraPosition.x + Random.Range(-_shakeMagnitude, _shakeMagnitude);
            float y = _originalCameraPosition.y + Random.Range(-_shakeMagnitude, _shakeMagnitude);

            _cameraTransform.position = new Vector3(x, y, _originalCameraPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        if (_cameraTransform != null)
        {
            _cameraTransform.position = _originalCameraPosition;
        }
    }

    /// <summary>
    /// Initiates the rhythm game, enabling gameplay elements and starting audio playback.
    /// Deactivates the prompt text once the game has started.
    /// </summary>
    void StartGame()
    {
        _startPlaying = true;
        _theBS.hasStarted = true;
        _audioSource.Play();
        _startText.gameObject.SetActive(false);
        _currentNoteStreak = 0;
    }

    bool IsGameOver()
    {
        GameObject[] noteObjects = GameObject.FindGameObjectsWithTag("Note");
        return noteObjects.Length == 0;
    }

    /// <summary>
    /// Handles the end of the game by stopping the audio and beat scroller, calculating percent of notes hit, 
    /// displaying the result panel, and awarding gold.
    /// </summary>
    void GameOver()
    {
        _audioSource.Stop();
        _theBS.hasStarted = false;
        _percentHit = CalculatePercentHit();
        _Resultpanel.ShowResults(_notesHit, _notesMissed, _noteStreak, _percentHit, _currentScore, _goldEarned);
        /* add gold earned to the player
         * 
         * player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.AddGold(goldEarned);
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
        */
    }

    /// <summary>
    /// Calculate the percent of notes hit in the game.
    /// </summary>
    private float CalculatePercentHit()
    {
        int totalNotes = _notesHit + _notesMissed;
        float percentHit = totalNotes > 0 ? (float)_notesHit / totalNotes * 100 : 0f;
        percentHit = (float)System.Math.Round(percentHit, 2);
        return percentHit;
    }

    /// <summary>
    /// Updates the multiplier based on the current note streak and multiplier thresholds.
    /// </summary>
    private void UpdateMultiplier()
    {
        if (_currentMultiplier - 1 < _multiplierThresholds.Length)
        {
            _multiplierTracker++;
            if (_multiplierThresholds[_currentMultiplier - 1] <= _multiplierTracker)
            {
                _multiplierTracker = 0;
                _currentMultiplier++;
            }
            _multiText.text = "Multiplier: x" + _currentMultiplier;
        }
    }

    /// <summary>
    /// Updates the player's score and updates the score text UI.
    /// Calculates gold earned based on score.
    /// </summary>
    private void UpdateScore()
    {
        _currentScore += _scorePerNote * _currentMultiplier;
        _scoreText.text = "Score: " + _currentScore;
        _goldEarned = _currentScore / 200;
    }

    /// <summary>
    /// Updates the current note streak by comparing it with the previous maximum streak.
    /// </summary>
    private void UpdateNoteStreak()
    {
        if (_currentNoteStreak > _noteStreak)
        {
            _noteStreak = _currentNoteStreak;
        }
    }

    /// <summary>
    /// Disables the player object at the start of the mini-game scene.
    /// </summary>
    private void DisablePlayerObject()
    {
        if (_playerObject != null)
        {
            _playerObject.SetActive(false);
        }
    }

    /// <summary>
    /// Reactivates the player object before leaving the mini-game scene.
    /// </summary>
    public void ReactivatePlayerObject()
    {
        if (_playerObject != null)
        {
            _playerObject.SetActive(true);
        }
    }
}