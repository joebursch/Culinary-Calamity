using UnityEngine;

public class QuestTarget : MonoBehaviour, InteractableObject
{
    public void Interact()
    {
        Debug.Log("I'm looking to see if you've completed my quest...");
    }
}