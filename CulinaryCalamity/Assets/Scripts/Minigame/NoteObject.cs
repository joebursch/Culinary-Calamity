using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false;
    public KeyCode keyToPress;
    private GameObject activator; // Reference to the activator object
    private Color originalColor; // Original color of the activator object
    private Vector3 originalCameraPosition; // Original position of the camera
    private Transform cameraTransform; // Reference to the camera transform
    private float shakeDuration = 0.2f; // Duration of the shake animation
    private float shakeMagnitude = 0.1f; // Magnitude of the shake

    void Start()
    {
        activator = GameObject.FindGameObjectWithTag("Activator"); // Find the activator object by tag
        originalColor = activator.GetComponent<SpriteRenderer>().color; // Store the original color
        cameraTransform = Camera.main.transform; // Get the main camera transform
        originalCameraPosition = cameraTransform.position; // Store the original position of the camera
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                MiniGameManager.instance.NoteHit();
                obtained = true;
                ChangeActivatorColor(Color.yellow); // Change activator color to yellow when hit
                Invoke("RevertActivatorColor", 0.1f); // Revert activator color back to original after a short delay
                gameObject.SetActive(false); // Deactivate the note object
            }
        }

        // Calculate the leftmost and rightmost positions of the note's sprite
        float leftEdge = transform.position.x - GetComponent<SpriteRenderer>().bounds.extents.x;
        float rightEdge = transform.position.x + GetComponent<SpriteRenderer>().bounds.extents.x;

        // Check if the note is obtained, and both its left and right edges are to the left of the camera's view
        if (!obtained && leftEdge < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x && rightEdge < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            Destroy(gameObject); // Destroy the note if it's obtained and completely out of the camera's view on the left side
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
            if (!obtained)
            {
                MiniGameManager.instance.NoteMissed();
                ChangeActivatorColor(Color.red);
                Invoke("RevertActivatorColor", 0.1f);
                StartCoroutine(ShakeScene()); // Trigger scene shaking animation when missed
            }
        }
    }

    void ChangeActivatorColor(Color color)
    {
        activator.GetComponent<SpriteRenderer>().color = color; // Change activator color
    }

    void RevertActivatorColor()
    {
        activator.GetComponent<SpriteRenderer>().color = originalColor; // Revert activator color back to original
    }

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
