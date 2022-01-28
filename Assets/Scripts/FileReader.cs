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
public class FileReader : MonoBehaviour
{
    // file
    private string file = "";

    // file path
    private string filePath = "";

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
        return File.Exists(filePath + file);
    }

    // read from the file.
    public void ReadFile()
    {
        // TODO: see if this works for clearing the array.
        lines = null;

        // gets all the lines from the file.
        string f = filePath + file;
        lines = File.ReadAllLines(@f);
    }

    // generates the doors from the saved lines.
    public void GenerateDoors()
    {

    }
}
