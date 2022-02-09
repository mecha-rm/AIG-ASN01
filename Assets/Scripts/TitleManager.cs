using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void ReadInputFields()
    {
        // sets the file path and file.
        fileReader.SetFilePath(filePathInput.text);
        fileReader.SetFile(fileInput.text);

        // disables the confirm text.
        readDefaultText.gameObject.SetActive(false);

        // attempts to read the file.
        if (fileReader.FileExists()) // file read
        {
            readSucessText.gameObject.SetActive(true);
            readFailText.gameObject.SetActive(false);
        }
        else // file not fou
        {
            readSucessText.gameObject.SetActive(false);
            readFailText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
