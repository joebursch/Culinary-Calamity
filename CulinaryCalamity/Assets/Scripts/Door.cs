using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, InteractableObject
{
    private Vector3 doorLocation;
    private Vector3 doorOthersideLocation;
    private bool unlocked = true;
    private bool playerInside = false;
    private string entryScene = "";

    // Start is called before the first frame update
    void Start()
    {
        doorLocation = transform.position;
        doorOthersideLocation = transform.position;
        // doorOthersideLocation = Other end TBD
    }

    // Update is called once per frame
    void Update()
    {
        if (entryScene.Equals(""))
        {
            if (PlayerAtDoor())
            {
                if (Unlocked())
                {
                    Open();
                }
                else
                {
                    // TODO Door is locked.
                }
            }
        }
        else
        {
            if (PlayerAtDoor())
            {
                if (Unlocked())
                {
                    if (PlayerInside())
                    {
                        SceneManager.UnloadSceneAsync("Home");
                        Open();
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync("Home");
                        Open();
                    }
                }
                else
                {
                    // TODO Door is locked.
                }
            }
        }
        if (PlayerAtDoor() & entryScene.Equals(""))
        {
            Open();
            GetComponent<Player>().transform.position = doorOthersideLocation;
            SceneManager.UnloadSceneAsync("Home");
        }
        else if(PlayerAtDoor() & !playerInside)
        {
            GetComponent<Player>().transform.position = doorOthersideLocation;
            SceneManager.LoadSceneAsync("Home");
        }
    }

    void PickUp()
    {
        // TODO Doors cannot be picked up
    }

    bool PlayerAtDoor()
    {
        return Vector3.Distance(GetComponent<Player>().transform.position, doorLocation) <= 5;
    }

    bool PlayerInside()
    {
        return playerInside;
    }

    bool Unlocked()
    {
        return unlocked;
    }

    void Use()
    {
        if (Unlocked())
        {
            if (PlayerAtDoor())
            {
                Open();
            }
        }
    }

    void Hurt()
    {
        // TODO Implement player damaging a door
    }

    void Open()
    {
        GetComponent<Player>().transform.position = doorOthersideLocation;
    }
}
