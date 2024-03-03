using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private Vector3 currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation = transform.position;
    }

    public override void PickUp()
    {
        // TODO Doors cannot be picked up
    }

    public override void Use()
    {
        if (playerCanUse)
        {
            // TODO Implement getting player location (construct player?)
            // if (Vector3.Distance(Player.GetPlayer().transform.position, transform.position) <= 5)
            // {
            //     // Door open
            // }
        }
    }

    public override void Hurt()
    {
        // TODO Implement player damaging a door
    }
}
