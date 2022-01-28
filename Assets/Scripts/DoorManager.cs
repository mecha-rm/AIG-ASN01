using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the door
public struct Door
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

// manages all doors.
public class DoorManager : MonoBehaviour
{
    // the list of doors.
    public List<Door> doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
