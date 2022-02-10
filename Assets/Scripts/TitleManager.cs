using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// manager for the title screen.
// TODO: disable start button when not available.
public class TitleManager : MonoBehaviour
{
    // the file reader.
    public FileReader fileReader;

    // user interface items.
    [Header("UI")]

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

    // drop down for changing the screen size.
    // this went unused.
    private Dropdown screenSizeDropdown;

    // the start button.
    public Button startButton;

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

        // sets to current screen size.
        if (screenSizeDropdown != null)
        {
            // if in full-screen, use option 0.
            if (Screen.fullScreen)
            {
                screenSizeDropdown.value = 0;
            }
            else // specifics screen size.
            {
                // checks current screen size.
                int screenY = Screen.height;

                // checks screen size to see default value.
                switch (screenY)
                {
                    case 1080: // big
                        screenSizeDropdown.value = 1;
                        break;

                    case 720: // medium
                        screenSizeDropdown.value = 2;
                        break;

                    case 480: // small
                        screenSizeDropdown.value = 3;
                        break;
                }
            }

        }

    }

    // called by dropdown.
    public void OnScreenSizeDropdownChange()
    {
        // get screen size from dropdown
        if (screenSizeDropdown != null)
            ChangeScreenSize(screenSizeDropdown.value);
    }

    // called when the screen size changes.
    public void ChangeScreenSize(int option)
    {
        switch (option)
        {
            case 0: // Full Screen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                break;

            case 1: // 1920 X 1080
                Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
                Screen.fullScreen = false;
                break;

            case 2: // 1280 X 720
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                Screen.fullScreen = false;
                break;

            case 3: // 854 X 480 (854 rounded up from 853.333)
                Screen.SetResolution(854, 480, FullScreenMode.Windowed);
                Screen.fullScreen = false;
                break;
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

        // checks if the file exists.
        bool exists = fileReader.FileExists();

        // attempts to read the file.
        if (exists) // file read
        {
            readSucessText.gameObject.SetActive(true);
            readFailText.gameObject.SetActive(false);
            startButton.interactable = true;
        }
        else // file not found
        {
            readSucessText.gameObject.SetActive(false);
            readFailText.gameObject.SetActive(true);
            startButton.interactable = false;
        }
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
