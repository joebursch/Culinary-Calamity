using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false;
    public bool createMode;
    public GameObject note;
    public KeyCode keyToPress;
    private GameObject activator; 
    private Color originalColor; 
    private Vector3 originalCameraPosition; 
    private Transform cameraTransform; 
    private float shakeDuration = 0.2f; 
    private float shakeMagnitude = 0.1f; 

    /// <summary>
    /// Initializes the note object by finding the activator object, storing its original color, and camera transform.
    /// </summary>
    void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Activator"); 
        originalColor = activator.GetComponent<SpriteRenderer>().color; 
        cameraTransform = Camera.main.transform; 
        originalCameraPosition = cameraTransform.position; 
    }

    /// <summary>
    /// Updates the note object's behavior each frame, including handling input for create mode or gameplay.
    /// </summary>
    void Update()
    {
        if (createMode)
        {
            if(Input.GetKeyDown(keyToPress))
            {
                Instantiate(note, transform.position, Quaternion.identity);
            }
        } else
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (canBePressed)
                {
                    MiniGameManager.instance.NoteHit();
                    obtained = true;
                    ChangeActivatorColor(Color.yellow); 
                    Invoke("RevertActivatorColor", 0.1f); 
                    gameObject.SetActive(false); 
                }
            }
        }

        // Calculate the leftmost and rightmost positions of the note's sprite
        float leftEdge = transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x;
        float rightEdge = transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x;

        // Check if the note is obtained, and both its left and right edges are to the left of the camera's view
        if (!obtained && leftEdge < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x 
            && !createMode && rightEdge < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            Destroy(gameObject); // Destroy the note if it's obtained and completely out of the camera's view on the left side
        }

    }

    /// <summary>
    /// Called when the note object enters a trigger zone to enable note pressing, only if not in create mode.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!createMode && other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    /// <summary>
    /// Called when the note object exits a trigger zone to disable note pressing, only if not in create mode.
    /// Triggers note missed event if note was not obtained.
    /// Starts the scene shaking animation if the note object is active.
    /// </summary>
    /// <param name="other"</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!createMode && other.CompareTag("Activator"))
        {
            canBePressed = false;
            if (!obtained)
            {
                MiniGameManager.instance.NoteMissed();
                ChangeActivatorColor(Color.red);
                Invoke("RevertActivatorColor", 0.1f);
                if (gameObject.activeSelf)
                {
                    StartCoroutine(ShakeScene());
                }
                    
            }
        }
    }

    /// <summary>
    /// Changes the color of the activator object to the specified color.
    /// </summary>
    /// <param name="color"></param>
    void ChangeActivatorColor(Color color)
    {
        activator.GetComponent<SpriteRenderer>().color = color; 
    }

    /// <summary>
    /// Reverts the color of the activator object back to its original color.
    /// </summary>
    void RevertActivatorColor()
    {
        activator.GetComponent<SpriteRenderer>().color = originalColor; 
    }

    /// <summary>
    /// Coroutine for shaking the scene with a random magnitude for a certain duration.
    /// </summary>
    IEnumerator ShakeScene()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = originalCameraPosition.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalCameraPosition.y + Random.Range(-shakeMagnitude, shakeMagnitude);

            cameraTransform.position = new Vector3(x, y, originalCameraPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraTransform.position = originalCameraPosition;
    }
}
