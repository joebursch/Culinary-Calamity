using System.Collections;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool startPlaying;
    [SerializeField] private BeatScroller theBS;
    [SerializeField] private int scorePerNote = 100;
    [SerializeField] private int[] multiplierThresholds;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private GameObject resultPanel;

    public static MiniGameManager instance;
    public bool createMode;

    private ResultPanel Resultpanel;
    private Player player;
    private Transform cameraTransform;
    private Vector3 originalCameraPosition;
    private float shakeDuration = 0.2f;
    private float shakeMagnitude = 0.1f;
    private int currentScore;
    private int currentMultiplier;
    private int multiplierTracker;
    private int notesHit;
    private int notesMissed;
    private int noteStreak;
    private int goldEarned;
    private int currentNoteStreak;
    private float percentHit;


    /// <summary>
    /// Initializes the MiniGameManager instance and sets up necessary components.
    /// </summary>
    void Start()
    {
        instance = this;

        startText.gameObject.SetActive(true);
        scoreText.text = "Score: 0 ";
        multiText.text = "Multiplier x1 ";
        noteStreak = 0; 
        currentNoteStreak = 0;
        currentMultiplier = 1;
        Resultpanel = resultPanel.GetComponent<ResultPanel>();
        resultPanel.SetActive(false);


        cameraTransform = Camera.main.transform;
        originalCameraPosition = cameraTransform.position;
    }

    /// <summary>
    /// Updates the game state every frame.
    /// </summary>
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                StartGame();
            }

        } else
        {
           if(IsGameOver())
            {
                audioSource.Stop();
                theBS.hasStarted = false;
                percentHit = CalculatePercentHit();
                Resultpanel.ShowResults(notesHit, notesMissed, noteStreak, percentHit, currentScore, goldEarned);

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
            notesHit++;
            currentNoteStreak++;

            if (currentNoteStreak > noteStreak)
            {
                noteStreak = currentNoteStreak;
            }

            if (currentMultiplier - 1 < multiplierThresholds.Length)
            {
                multiplierTracker++;

                if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
                {
                    multiplierTracker = 0;
                    currentMultiplier++;
                }

                multiText.text = "Multiplier: x" + currentMultiplier;

                currentScore += scorePerNote * currentMultiplier;
                scoreText.text = "Score: " + currentScore;

                goldEarned = currentScore / 200;
            }
        }
    }

    /// <summary>
    /// Handles missed notes by resetting the multiplier and updating the UI.
    /// </summary>
    public void NoteMissed()
    {
        if (!createMode) 
        {
            notesMissed++;
            currentMultiplier = 1;
            multiplierTracker = 0;

            multiText.text = "Multiplier: x" + currentMultiplier;

            currentNoteStreak = 0;
        }
    }

    /// <summary>
    /// Coroutine for shaking the camera to add visual effect.
    /// </summary>
    /// <returns>An IEnumerator for Unity coroutine.</returns>
    public IEnumerator ShakeScene()
    {
        if (cameraTransform == null)
        {
            yield break; 
        }

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            if (cameraTransform == null)
            {
                yield break; 
            }
            float x = originalCameraPosition.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalCameraPosition.y + Random.Range(-shakeMagnitude, shakeMagnitude);

            cameraTransform.position = new Vector3(x, y, originalCameraPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        if (cameraTransform != null)
        {
            cameraTransform.position = originalCameraPosition;
        }
    }

    /// <summary>
    /// Initiates the rhythm game, enabling gameplay elements and starting audio playback.
    /// Deactivates the prompt text once the game has started.
    /// </summary>
    void StartGame()
    {
        startPlaying = true;
        theBS.hasStarted = true;
        audioSource.Play();
        startText.gameObject.SetActive(false);

        currentNoteStreak = 0;
    }
   
    /// <summary>
    /// Checks if the game is over by determining if there are any remaining notes.
    /// </summary>
    /// <returns>True if the game is over, false otherwise.</returns>
    bool IsGameOver()
    {
        GameObject[] noteObjects = GameObject.FindGameObjectsWithTag("Note");
        
        return noteObjects.Length == 0;
    }

    /// <summary>
    /// Calculate the percent of notes hit in the game.
    /// </summary>
    private float CalculatePercentHit()
    {
        int totalNotes = notesHit + notesMissed;
        float percentHit = totalNotes > 0 ? (float)notesHit / totalNotes * 100 : 0f;
        percentHit = (float)System.Math.Round(percentHit, 2);
        Debug.Log("Percentage Hit: " + percentHit);

        return percentHit;
    }

}
