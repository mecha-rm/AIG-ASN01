using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the door entry
public struct DoorEntry
{
    // if 'true', the door is hot.
    public bool hot;

    // if 'true', noise is heard behind the door.
    public bool noisy;

    // if 'true', the door is safe.
    public bool safe;

    // percentage of doors (0.0 - 1.0)
    public float percent;
}

// a door object for the game. 
public class Door : MonoBehaviour
{
    // if 'true', the door is hot.
    public bool hot;

    // if 'true', noise is heard behind the door.
    public bool noisy;

    // if 'true', the door is safe. If 'false', it's not safe.
    public bool safe;

    // if 'true', the door is open.
    public bool open;

    [Header("Sprites")]
    // sprite for a closed door
    public Sprite closedDoor;

    // sprite for an open door.
    public Sprite openDoor;

    [Header("Other")]

    // the gameplay manager for the object.
    GameplayManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: search for door.
        if(closedDoor == null || openDoor == null)
        {

        }

        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // called when the door is collided with.
    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void OpenDoor()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
