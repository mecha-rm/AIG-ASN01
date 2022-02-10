/*
 * References:
 * - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-read-from-a-text-file
 * - https://support.unity.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
 * - https://docs.microsoft.com/en-us/dotnet/api/system.io.file.exists?view=net-6.0
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


// file reader for excel text file exports.
// NOTE: if the file is in the Assets folder (or a subfolder of the Assets folder), you can just do it from there.
public class FileReader : MonoBehaviour
{
    // file
    public string file = "";

    // file path
    public string filePath = "";

    // the lines from the file.
    public string[] lines;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // sets the file.
    public void SetFile(string newFile)
    {
        file = newFile;
    }

    // sets the file and the file path.
    public void SetFile(string newFile, string newFilePath)
    {
        SetFile(newFile);
        SetFilePath(newFilePath);
    }

    // sets the file path.
    public void SetFilePath(string newFilePath)
    {
        // set new file path.
        filePath = newFilePath;

        // if the file path is not empty.
        if (filePath.Length != 0)
        {
            // if the last character is not a slash, add one.
            if (filePath[filePath.Length - 1] != '\\')
            {
                filePath += "\\";
            }
        }
    }

    // sets the file path and the file.
    public void SetFilePath(string newFilePath, string newFile)
    {
        SetFilePath(newFilePath);
        SetFile(newFile);
    }

    // checks if the file exists.
    public bool FileExists()
    {
        // sets the file and file path to make sure they're formatted properly.
        SetFile(file, filePath);

        // returns true if the file exists.
        return File.Exists(filePath + file);
    }

    // read from the file.
    public void ReadFile()
    {
        // checks if the file exists.
        if(!FileExists())
        {
            Debug.LogError("File does not exist.");
            return;
        }

        // TODO: see if this works for clearing the array. C# garbage collection should take care of this?
        lines = null;

        // gets all the lines from the file.
        string f = filePath + file;
        lines = File.ReadAllLines(@f);
    }

    // generates the doors from the saved lines.
    public List<DoorEntry> GenerateDoors()
    {
        // checking for lines to read from.
        bool valid = lines != null;

        // attemps to read the file if it hasn't been read yet.
        if (!valid)
            ReadFile();

        // if the line object exists, but nothing has been read yet.
        if (valid && lines.Length == 0)
            ReadFile();

        valid = lines != null;

        // are there lines to possibly read?
        if (valid)
        {
            // no lines to read from.
            if (lines.Length == 0)
            {
                Debug.LogError("No door data file loaded. Could not generate doors. Returning null.");
                return null;

            }
        }
        else
        {
            Debug.LogError("File load failed, so there are no lines to read. Returning null.");
            return null;
        }


        // makes list of doors.
        List<DoorEntry> doors = new List<DoorEntry>();

        // goes through all lines, skipping the first line since it's the header.
        for(int i = 1; i < lines.Length; i++)
        {
            string[] str = lines[i].Split('\t'); // splits the string

            // the string should have four elements.
            if(str.Length == 4)
            {
                DoorEntry door = new DoorEntry(); // makes a new door.

                door.hot = (str[0] == "Y"); // is the door hot?
                door.noisy = (str[1] == "Y"); // is noisy being heard from behind the door?
                door.safe = (str[2] == "Y"); // is the door safe?

                // the percentage
                door.percent = float.Parse(str[3]);

                // adds door to list of doors
                doors.Add(door);
            }
        }

        return doors;
    }
}
