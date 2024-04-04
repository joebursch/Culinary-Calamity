using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false;
    public KeyCode keyToPress;
    private Activator activator;
    public GameObject note;

    void Start()
    {
        // Find the Activator GameObject and store its reference
        activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
    }

    void Update()
    {
        if (MiniGameManager.instance.createMode)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                Instantiate(note, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Input.GetKeyDown(keyToPress))
            {
                if (canBePressed)
                {
                    MiniGameManager.instance.NoteHit();
                    obtained = true;
                    activator.ChangeColorWithDelay(Color.yellow, 0.1f);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == activator.gameObject)
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!MiniGameManager.instance.createMode && other.gameObject == activator.gameObject)
        {
            canBePressed = false;
            if (!obtained)
            {
                MiniGameManager.instance.NoteMissed();
                activator.ChangeColorWithDelay(Color.red, 0.1f);
                if (MiniGameManager.instance.gameObject.activeSelf) // Check if MiniGameManager GameObject is active
                {
                    StartCoroutine(MiniGameManager.instance.ShakeScene());
                }
            }
        }
    }

}
