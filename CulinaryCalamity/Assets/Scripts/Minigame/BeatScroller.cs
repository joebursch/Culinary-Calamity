using UnityEngine;

/// <summary>
/// Manages the scrolling of notes in the mini-game.
/// </summary>
public class BeatScroller : MonoBehaviour
{
    [SerializeField] private float beatTempo;
    public bool hasStarted;

    /// <summary>
    /// Initializes the beat tempo by converting it to seconds per beat.
    /// </summary>
    void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    /// <summary>
    /// Updates the beat scrolling each frame.
    /// </summary>
    void Update()
    {
        if(hasStarted)
        {
            transform.position -= new Vector3(beatTempo * Time.deltaTime, 0f, 0f);
        }
    }
}
