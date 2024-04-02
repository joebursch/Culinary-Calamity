using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public bool startPlaying;
    public BeatScroller theBS;

    public static MiniGameManager instance;

    public int currentScore;

    public int scorePerNote = 100;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI multiText;



    /// <summary>
    /// Initializes the singleton instance. Starts the beat scroller
    /// </summary>
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0 ";
        multiText.text = "Multiplier x1 ";
        currentMultiplier = 1;

        theBS.hasStarted = true;
    }

    /// <summary>
    /// Handles the event when a note is successfully hit.
    /// Increases the score based on the current multiplier and score per note,
    /// updates the multiplier if necessary, and updates the score text UI.
    /// </summary>
    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (!startPlaying)
        {
            StartGame();
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
        }

    }

    /// <summary>
    /// Handles the event when a note is missed.
    /// Resets the current multiplier and multiplier tracker,
    /// updates the multiplier text UI, and prepares for the next note.
    /// </summary>
    public void NoteMissed()
    {
        Debug.Log("Missed note");

        if (!startPlaying)
        {
            StartGame();
        }

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    /// <summary>
    /// Starts the game by playing the song.
    /// </summary>
    void StartGame()
    {
        startPlaying = true;
        audioSource.Play();
    }
}
