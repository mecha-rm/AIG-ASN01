using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the gameplay manager.
public class GameplayManager : MonoBehaviour
{
    List<Door> doors;

    // Start is called before the first frame update
    void Start()
    {
        // FILE READ TEST
        FileReader fr = new FileReader();
        fr.file = "probabilities.txt";
        
        // both methods work.
        // the full path (absolute path)
        fr.filePath = "C:\\Users\\roder\\source\\year 4 repos\\AIG-ASN01\\Assets\\Resources\\Imports";
        
        // the path from the project file
        fr.filePath = "Assets\\Resources\\Imports";
        
        Debug.Log("File Exists? - " + fr.FileExists());
        doors = fr.GenerateDoors();

        if (doors == null)
            Debug.Log("No doors loaded.");
        else
            Debug.Log("Doors: " + doors.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
