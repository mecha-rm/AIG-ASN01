using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
