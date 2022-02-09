using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// loads the game information when entering a scene.
public class GameLoader : MonoBehaviour
{
    // the file being loaded from.
    public string file = "probabilities.txt";

    // the file path the loaded file is from.
    public string filePath = "Assets\\Resources\\Imports";

    // the list of doors.
    public List<Door> doors = new List<Door>();

    // the total amount of doors.
    public int doorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // don't destroy this object on load.
        DontDestroyOnLoad(gameObject);
    }
}
