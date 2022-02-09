using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manager for the title screen.
// TODO: disable start button when not available.
public class TitleManager : MonoBehaviour
{
    // field for inputting the file path.
    public InputField filePathInput;

    // field for inputting the file.
    public InputField fileInput;

    // text for confirming the text file.
    public Text readDefaultText;

    // text for successful file read.
    public Text readSucessText;

    // text for failure of file read.
    public Text readFailText;

    // the file reader.
    public FileReader fileReader;

    // Start is called before the first frame update
    void Start()
    {
        // makes a new file reader if not set.
        if (fileReader == null)
        {
            // tries to get the file reader. If it fails, then add the component.
            if(!TryGetComponent<FileReader>(out fileReader))
                fileReader = gameObject.AddComponent<FileReader>();
        }
            
    }

    // called by the button in the scene to read the input field.
    public bool ReadInputFields()
    {
        // sets the file path and file.
        fileReader.SetFilePath(filePathInput.text);
        fileReader.SetFile(fileInput.text);

        // disables the confirm text.
        readDefaultText.gameObject.SetActive(false);

        // checks if the file exists.
        bool exists = fileReader.FileExists();

        // attempts to read the file.
        if (exists) // file read
        {
            readSucessText.gameObject.SetActive(true);
            readFailText.gameObject.SetActive(false);
        }
        else // file not found
        {
            readSucessText.gameObject.SetActive(false);
            readFailText.gameObject.SetActive(true);
        }

        return exists;
    }

    // starts the game.
    public void StartGame()
    {
        // checks if the file exists.
        if (!fileReader.FileExists())
            return;

        // finds the loader.
        GameLoader loader = FindObjectOfType<GameLoader>();

        // no loader currently exists, so make one.
        if(loader == null)
        {
            GameObject go = new GameObject();
            loader = go.AddComponent<GameLoader>();
        }

        // save values to the loader.
        loader.file = fileReader.file;
        loader.filePath = fileReader.filePath;
        loader.doorsEntries = fileReader.GenerateDoors();
        loader.doorCount = 36;

        // goes to the game scene.
        SceneHelper.ChangeScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
