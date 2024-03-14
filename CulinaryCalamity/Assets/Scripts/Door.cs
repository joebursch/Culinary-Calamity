using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour, InteractableObject
{
    [SerializeField] private Vector3 doorOthersideLocation;
    [SerializeField] private bool unlocked = true;
    private bool playerInside = false;
    private string entryScene = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void PickUp()
    {
        // TODO Doors cannot be picked up
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == GetComponent<Player>())
        {
            Use();
        }
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
        if (entryScene.Equals(""))
        { 
           if (Unlocked())
                {
                    Open();
                    playerInside = true;
                }
                else
                {
                    // TODO Door is locked.
                }
        }
        else
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

    void Hurt()
    {
        // TODO Implement player damaging a door
    }

    void Open()
    {
        GetComponent<Player>().transform.position = doorOthersideLocation;
    }
}
