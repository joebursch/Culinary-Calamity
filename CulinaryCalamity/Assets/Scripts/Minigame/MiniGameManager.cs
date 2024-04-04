using System.Collections;
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


    private Transform cameraTransform;
    private Vector3 originalCameraPosition;
    private float shakeDuration = 0.2f;
    private float shakeMagnitude = 0.1f;

    
    public bool createMode;

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0 ";
        multiText.text = "Multiplier x1 ";
        currentMultiplier = 1;

        // Assign camera transform and original position
        cameraTransform = Camera.main.transform;
        originalCameraPosition = cameraTransform.position;
    }

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

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (!createMode)  // Only process score and multiplier logic if not in create mode
        {
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
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note");


        if (!createMode)  // Only reset multiplier if not in create mode
        {
            currentMultiplier = 1;
            multiplierTracker = 0;

            multiText.text = "Multiplier: x" + currentMultiplier;
        }
    }

    // New function for camera shake
    public IEnumerator ShakeScene()
    {
        if (cameraTransform == null)
        {
            yield break; // Exit the coroutine if cameraTransform is null
        }

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            if (cameraTransform == null)
            {
                yield break; // Exit the coroutine if cameraTransform is null
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
}
