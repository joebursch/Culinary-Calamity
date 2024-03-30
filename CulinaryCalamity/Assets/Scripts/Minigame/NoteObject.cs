using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public bool obtained = false;

    public KeyCode keyToPress;

    /// <summary>
    /// Start is called before the first frame update. 
    /// It is used for initialization purposes and does not contain any specific functionality in this context.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame. Handles input for pressing the note and checks if it can be pressed.
    /// If the note can be pressed and the corresponding key is pressed, triggers the NoteHit function, sets the note as obtained, and deactivates the game object.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                MiniGameManager.instance.NoteHit();
                obtained = true;
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this GameObject.
    /// </summary>
    /// <param name="other">The other collider.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger collider attached to this GameObject.
    /// </summary>
    /// <param name="other">The other collider.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
            if(!obtained)
            {
                MiniGameManager.instance.NoteMissed();
            }
        }
    }
}
