using Dialogue;
using UnityEngine;

/// <summary>
/// Doors class.
/// </summary>
public class Door : MonoBehaviour, InteractableObject
{
    [SerializeField] Vector3 destinationLocation;
    [SerializeField] bool unlocked = true;
    [SerializeField] bool active = true;
    [SerializeField] string entranceSceneName = "";
    [SerializeField] string destinationSceneName = "";
    [SerializeField] private TextAsset _doorDialogue = null;

    /// <summary>
    /// When the door interacts with something.
    /// </summary>
    public void Interact()
    {
        if (_doorDialogue != null && !unlocked) { DialogueManager.GetDialogueManager().InitializeDialogue(_doorDialogue); }
        else
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Method to get the door's destination location.
    /// </summary>
    /// <returns>Vector3: The door's destination location</returns>
    public Vector3 GetDestinationLocation()
    {
        return destinationLocation;
    }

    /// <summary>
    /// Method to set the door's destination scene.
    /// </summary>
    public void SetDestinationScene(string scene)
    {
        destinationSceneName = scene;
    }

    /// <summary>
    /// Method to set the door's start scene.
    /// </summary>
    public void SetEntranceScene(string scene)
    {
        entranceSceneName = scene;
    }

    /// <summary>
    /// Method to set the door's unlocked status.
    /// </summary>
    public void setUnlocked(bool unlocked)
    {
        this.unlocked = unlocked;
    }

    /// <summary>
    /// Method to check if the door is unlocked.
    /// </summary>
    /// <returns>bool: True if door is unlocked, false otherwise.</returns>
    public bool IsUnlocked()
    {
        return unlocked;
    }

    /// <summary>
    /// Method to get the name of the scene the door is located.
    /// </summary>
    /// <returns>string: The string of the name of the scene the door is located.</returns>
    public string GetEntranceSceneName()
    {
        return entranceSceneName;
    }

    /// <summary>
    /// Method to get the name of the scene of the door's destination.
    /// </summary>
    /// <returns>string: The string of the name of the scene of the door's destination.</returns>
    public string GetDestinationSceneName()
    {
        return destinationSceneName;
    }

    /// <summary>
    /// Method to check whether the door is active or passive.
    /// </summary>
    /// <returns>bool: True if door is active, false if passive.</returns>
    public bool IsActive()
    {
        return active;
    }


    public void MovePlayer(Transform playerTransform)
    {
        playerTransform.position = GetDestinationLocation();
    }
}
