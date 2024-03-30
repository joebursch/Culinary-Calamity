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
    /// Initializes the singleton instance.
    /// </summary>
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0 ";
        multiText.text = "Multiplier x1 ";
        currentMultiplier = 1;
    }


    /// <summary>
    /// Update is called once per frame. Starts the mini-game when any key is pressed.
    /// </summary>
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                audioSource.Play();
            }
        }
    }

    /// <summary>
    /// Called when a note is hit.
    /// </summary>
    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if(currentMultiplier - 1 < multiplierThresholds.Length)
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
    /// Called when a note is missed.
    /// </summary>
    public void NoteMissed()
    {
        Debug.Log("Missed note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
    }
}
