using UnityEngine;


public class Door : MonoBehaviour, InteractableObject
{
    [SerializeField] Vector3 destinationLocation;
    [SerializeField] bool unlocked = true;
    [SerializeField] bool active = true;
    [SerializeField] string entranceSceneName = "";
    [SerializeField] string destinationSceneName = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetDestinationLocation()
    {
        return destinationLocation;
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    public string GetEntranceSceneName()
    { 
        return entranceSceneName; 
    }

    public string GetDestinationSceneName()
    { 
        return destinationSceneName;
    }

    public bool IsActive()
    {
        return active;
    }
}
