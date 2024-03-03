using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, InteractableObject
{
    private Vector3 doorLocation;
    private Vector3 doorOthersideLocation;
    private bool unlocked = true;

    // Start is called before the first frame update
    void Start()
    {
        doorLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        doorLocation = transform.position;
    }

    public void PickUp()
    {
        // TODO Doors cannot be picked up
    }

    public void Use()
    {
        if (unlocked)
        {
            if (Vector3.Distance(GetComponent<Player>().transform.position, doorLocation) <= 5)
            {
                Open();
            }
        }
    }

    public void Hurt()
    {
        // TODO Implement player damaging a door
    }

    void Open()
    {
        GetComponent<Player>().transform.position = doorLocation;
    }
}
